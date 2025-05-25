using FinancialApp.Data;
using FinancialApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinancialApp.Controllers
{
    [Authorize]
    public class TransactionController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new InvalidOperationException("User ID claim is missing. Make sure the user is authenticated.");
            return int.Parse(userIdClaim);
        }



        private void PopulateDropdowns(int userId)
        {
            ViewBag.Accounts = new SelectList(_context.Accounts.Where(a => a.UserId == userId), "Id", "AccountName");
            ViewBag.Categories = new SelectList(_context.TransactionCategories.Where(c => c.UserId == userId), "Id", "Name");
            ViewBag.AccountId = ViewBag.Accounts;
            ViewBag.CategoryId = ViewBag.Categories;
        }

        public IActionResult Index(int? accountId)
        {
            int userId = GetCurrentUserId();
            var transactions = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .Where(t => t.Account != null && t.Account.UserId == userId)
                .OrderByDescending(t => t.CreatedAt);

            if (accountId.HasValue)
                transactions = (IOrderedQueryable<Transaction>)transactions.Where(t => t.AccountId == accountId);

            ViewBag.Accounts = new SelectList(_context.Accounts.Where(a => a.UserId == userId), "Id", "AccountName");

            return View(transactions.ToList());
        }

        // =========================
        // CREATE DEPOSIT (GET/POST)
        // =========================

        public IActionResult CreateDeposit()
        {
            int userId = GetCurrentUserId();
            PopulateDropdowns(userId);
            return View();
        }

        [HttpPost]
        public IActionResult CreateDeposit(Transaction model)
        {
            int userId = GetCurrentUserId();

            model.Type = "Deposit";

            if (string.IsNullOrWhiteSpace(model.Type))
            {
                ModelState.AddModelError(nameof(model.Type), "Type is required.");
            }

            Console.WriteLine("Model.Type BEFORE validation: " + model.Type);

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                PopulateDropdowns(userId);
                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;
            model.UserId = userId;

            var account = _context.Accounts.FirstOrDefault(a => a.Id == model.AccountId && a.UserId == userId);
            if (account == null)
            {
                ModelState.AddModelError("AccountId", "Account not found.");
                PopulateDropdowns(userId);
                return View(model);
            }

            account.Balance += model.Amount;
            _context.Transactions.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult CreateWithdrawal()
        {
            int userId = GetCurrentUserId();
            PopulateDropdowns(userId);
            return View();
        }

        [HttpPost]
        public IActionResult CreateWithdrawal(Transaction model)
        {
            int userId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(userId);
                return View(model);
            }

            var account = _context.Accounts.FirstOrDefault(a => a.Id == model.AccountId && a.UserId == userId);
            if (account == null)
            {
                ModelState.AddModelError("AccountId", "Account not found.");
                PopulateDropdowns(userId);
                return View(model);
            }

            if (account.Balance < model.Amount)
            {
                ModelState.AddModelError("Amount", "Insufficient balance.");
                PopulateDropdowns(userId);
                return View(model);
            }

            model.Type = "Withdrawal";
            model.CreatedAt = DateTime.UtcNow;
            model.UserId = userId;

            account.Balance -= model.Amount;
            _context.Transactions.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // =============================
        // CREATE TRANSFER (GET/POST)
        // =============================

        public IActionResult CreateTransfer()
        {
            int userId = GetCurrentUserId();
            PopulateDropdowns(userId);
            return View();
        }

        [HttpPost]
        public IActionResult CreateTransfer(int fromAccountId, int toAccountId, decimal amount, string description)
        {
            int userId = GetCurrentUserId();

            if (fromAccountId == toAccountId)
            {
                ModelState.AddModelError("", "Cannot transfer to the same account.");
                PopulateDropdowns(userId);
                return View();
            }

            var fromAccount = _context.Accounts.FirstOrDefault(a => a.Id == fromAccountId && a.UserId == userId);
            var toAccount = _context.Accounts.FirstOrDefault(a => a.Id == toAccountId && a.UserId == userId);

            if (fromAccount == null || toAccount == null)
            {
                ModelState.AddModelError("", "One or both accounts not found.");
                PopulateDropdowns(userId);
                return View();
            }

            if (fromAccount.Balance < amount)
            {
                ModelState.AddModelError("", "Insufficient balance.");
                PopulateDropdowns(userId);
                return View();
            }

            var timestamp = DateTime.UtcNow;

            var withdraw = new Transaction
            {
                AccountId = fromAccountId,
                Type = "Transfer",
                Amount = amount,
                Description = $"Transfer to {toAccount.AccountName}: {description}",
                CreatedAt = timestamp,
                UserId = userId
            };

            var deposit = new Transaction
            {
                AccountId = toAccountId,
                Type = "Transfer",
                Amount = amount,
                Description = $"Transfer from {fromAccount.AccountName}: {description}",
                CreatedAt = timestamp,
                UserId = userId
            };

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _context.Transactions.AddRange(withdraw, deposit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var txn = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id);
            if (txn == null) return NotFound();
            return View(txn);
        }

        // ===================
        // EDIT TRANSACTION
        // ===================

        public IActionResult Edit(int id)
        {
            int userId = GetCurrentUserId();

            var transaction = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id && t.Account != null && t.Account.UserId == userId);

            if (transaction == null)
                return NotFound();

            PopulateDropdowns(userId);
            return View(transaction);
        }

        [HttpPost]
        public IActionResult Edit(int id, Transaction updatedTransaction)
        {
            int userId = GetCurrentUserId();

            if (id != updatedTransaction.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(userId);
                return View(updatedTransaction);
            }

            var transaction = _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefault(t => t.Id == id && t.Account != null && t.Account.UserId == userId);

            if (transaction == null)
                return NotFound();

            transaction.Type = updatedTransaction.Type;
            transaction.Amount = updatedTransaction.Amount;
            transaction.Description = updatedTransaction.Description;
            transaction.CategoryId = updatedTransaction.CategoryId;
            transaction.AccountId = updatedTransaction.AccountId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Transaction/Delete/5
        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id && t.Account != null && t.Account.UserId == GetCurrentUserId());

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefault(t => t.Id == id && t.Account != null && t.Account.UserId == GetCurrentUserId());

            if (transaction == null)
                return NotFound();

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}