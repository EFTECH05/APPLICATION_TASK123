using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    public class AdminPaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // ================= ADMIN PAYMENTS LIST =================
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            var user = HttpContext.Session.GetString("User");

            // ================= AUTH CHECK =================
            if (string.IsNullOrEmpty(user))
                return RedirectToAction("Login", "Auth");

            if (role != "Admin" && role != "SuperAdmin")
                return Forbid();

            // ================= LOAD PAYMENTS + USER =================
            var payments = _context.Payments
                .Include(p => p.User)   // IMPORTANT: requires User navigation property
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            return View(payments);
        }
    }
}