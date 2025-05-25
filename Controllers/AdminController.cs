using FinancialApp.Data;
using FinancialApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinancialApp.Controllers
{
    public class AdminController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Transactions()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Account) // Fix: Replace 'User' with 'Account' or another valid navigation property
                .ToListAsync();
            return View(transactions);
        }

        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(int id, User updated)
        {
            if (id != updated.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound();

                user.Email = updated.Email;
                user.Role = updated.Role;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Users));
            }
            return View(updated);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Users));
        }

    }
}
