using FinancialApp.Data;
using FinancialApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinancialApp.Controllers
{
    [Authorize]
    public class AccountController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: /Account
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole("Admin");

            var accounts = isAdmin
                ? await _context.Accounts.Include(a => a.User).ToListAsync()
                : await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();

            return View(accounts);
        }

        // GET: /Account/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null || !CanAccess(account.UserId))
                return NotFound();

            return View(account);
        }

        // GET: /Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // or use a logger
                }
                return View(model);
            }



            model.UserId = GetCurrentUserId();
            model.CreatedAt = DateTime.UtcNow;
            model.Balance = 0;

            _context.Accounts.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Account created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Account/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null || !CanAccess(account.UserId))
                return NotFound();

            return View(account);
        }

        // POST: /Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Account model)
        {
            if (id != model.Id)
                return NotFound();

            var account = await _context.Accounts.FindAsync(id);
            if (account == null || !CanAccess(account.UserId))
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            account.AccountName = model.AccountName;
            account.AccountType = model.AccountType;

            _context.Update(account);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Account updated.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Account/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null || !CanAccess(account.UserId))
                return NotFound();

            return View(account);
        }

        // POST: /Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null || !CanAccess(account.UserId))
                return NotFound();

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Account deleted.";
            return RedirectToAction(nameof(Index));
        }

        // --- Helpers ---
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new InvalidOperationException("User ID claim is missing.");

            return int.Parse(userIdClaim);
        }

        private bool CanAccess(int ownerId)
        {
            return User.IsInRole("Admin") || GetCurrentUserId() == ownerId;
        }
    }
}
