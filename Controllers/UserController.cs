using FinancialApp.Data;
using FinancialApp.Models;
using FinancialApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.PasswordHash))
            {
                ModelState.AddModelError("PasswordHash", "Password cannot be null or empty.");
                return View(model);
            }

            model.PasswordHash = PasswordHelper.HashPassword(model.PasswordHash); // ✅ Use your custom hashing
            model.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            var model = new User
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(User model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }


            var user = _context.Users.Find(model.Id);
            if (user == null) return NotFound();

            // Only update the fields that should be editable
            user.Email = model.Email;
            user.Role = model.Role;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult AssignRoles(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult AssignRoles(int id, string role)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Role = role;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
