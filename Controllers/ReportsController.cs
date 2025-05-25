using FinancialApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinancialApp.Controllers
{
    [Authorize]
    public class ReportController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult MonthlySummary(int? year, int? month)
        {
            var userId = GetCurrentUserId();
            var today = DateTime.UtcNow;

            int selectedYear = year ?? today.Year;
            int selectedMonth = month ?? today.Month;

            var transactions = _context.Transactions
                .Include(t => t.Account)
                .Where(t => t.Account != null && // Ensure Account is not null
                            t.Account.UserId == userId &&
                            t.CreatedAt.Year == selectedYear &&
                            t.CreatedAt.Month == selectedMonth)
                .ToList();

            var summary = new
            {
                Year = selectedYear,
                Month = selectedMonth,
                TotalDeposits = transactions.Where(t => t.Type == "Deposit").Sum(t => t.Amount),
                TotalWithdrawals = transactions.Where(t => t.Type == "Withdrawal").Sum(t => t.Amount),
                NetChange = transactions.Sum(t => t.Type == "Deposit" ? t.Amount : -t.Amount)
            };

            ViewBag.Transactions = transactions;
            return View(summary);
        }

        public IActionResult BalanceOverview()
        {
            var userId = GetCurrentUserId();
            var accounts = _context.Accounts
                                   .Where(a => a.UserId == userId)
                                   .ToList();
            return View(accounts);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new InvalidOperationException("User ID claim is missing.");

            return int.Parse(userIdClaim);
        }

        [HttpGet]
        public IActionResult ExportMonthlySummary(int? year, int? month)
        {
            var userId = GetCurrentUserId();
            var today = DateTime.UtcNow;
            int selectedYear = year ?? today.Year;
            int selectedMonth = month ?? today.Month;

            var transactions = _context.Transactions
                .Include(t => t.Account)
                .Where(t => t.Account != null &&
                            t.Account.UserId == userId &&
                            t.CreatedAt.Year == selectedYear &&
                            t.CreatedAt.Month == selectedMonth)
                .ToList();

            var csv = new StringWriter();
            csv.WriteLine("Date,Account,Type,Amount,Description");

            foreach (var t in transactions)
            {
                csv.WriteLine($"{t.CreatedAt:yyyy-MM-dd},{t.Account?.AccountName ?? "Unknown"},{t.Type},{t.Amount},{t.Description ?? "No Description"}");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"MonthlySummary_{selectedYear}_{selectedMonth}.csv");
        }

    }
}
