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

            var payments = _context.Payments
                .Include(p => p.User)
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            return View(payments);
        }

        // ================= APPROVE PAYMENT =================
        [HttpPost]
        public IActionResult Approve(int id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin" && role != "SuperAdmin")
                return Forbid();

            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment == null)
                return NotFound();

            payment.Status = "Approved";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ================= REJECT PAYMENT =================
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin" && role != "SuperAdmin")
                return Forbid();

            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment == null)
                return NotFound();

            payment.Status = "Rejected";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}