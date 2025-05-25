using FinancialApp.Data;
using FinancialApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinancialApp.Controllers
{
    [Authorize]
    public class CategoryController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            var userId = GetCurrentUserId();
            var categories = _context.TransactionCategories
                                     .Where(c => c.UserId == userId)
                                     .ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionCategory category)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // or use a logger
                }

                return View(category);
            }


            category.UserId = GetCurrentUserId();
            category.CreatedAt = DateTime.UtcNow;

            // Do NOT set 'User' here – let EF only track by foreign key
            _context.TransactionCategories.Add(category);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var userId = GetCurrentUserId();
            var category = _context.TransactionCategories
                                   .FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(TransactionCategory category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var existing = _context.TransactionCategories
                                   .FirstOrDefault(c => c.Id == category.Id && c.UserId == GetCurrentUserId());
            if (existing == null)
                return NotFound();

            existing.Name = category.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var userId = GetCurrentUserId();
            var category = _context.TransactionCategories
                                   .FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (category == null)
                return NotFound();

            _context.TransactionCategories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                throw new InvalidOperationException("User ID claim is missing.");

            return int.Parse(userIdClaim);
        }

    }
}
