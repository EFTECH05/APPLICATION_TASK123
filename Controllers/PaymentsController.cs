using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // ================= CREATE PAYMENT =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            // ================= SERVER SECURITY =================
            if (model.Amount <= 0)
            {
                TempData["Error"] = "Invalid amount";
                return RedirectToAction("Index", "UserDashboard");
            }

            model.UserId = userId.Value;
            model.Status = "Pending";
            model.DateCreated = DateTime.Now;

            _context.Payments.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Payment submitted successfully!";
            return RedirectToAction("Index", "UserDashboard");
        }
    }
}