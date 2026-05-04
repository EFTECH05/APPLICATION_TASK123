using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;
using System.Text.RegularExpressions;

namespace PaymentSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // ================= LOAD PAYMENT FORM =================
        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // ================= PROCESS PAYMENT =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            // ================= VALIDATION =================
            if (!Regex.IsMatch(model.RecipientName ?? "", @"^[a-zA-Z\s]{3,100}$"))
            {
                TempData["Error"] = "Invalid recipient name";
                return RedirectToAction("Index", "UserDashboard");
            }

            if (!Regex.IsMatch(model.AccountNumber ?? "", @"^[0-9]{6,20}$"))
            {
                TempData["Error"] = "Invalid account number";
                return RedirectToAction("Index", "UserDashboard");
            }

            if (!Regex.IsMatch(model.BankName ?? "", @"^[a-zA-Z\s]{3,100}$"))
            {
                TempData["Error"] = "Invalid bank name";
                return RedirectToAction("Index", "UserDashboard");
            }

            if (!Regex.IsMatch(model.SwiftCode ?? "", @"^[A-Z0-9]{8,11}$"))
            {
                TempData["Error"] = "Invalid SWIFT code";
                return RedirectToAction("Index", "UserDashboard");
            }

            if (model.Amount <= 0)
            {
                TempData["Error"] = "Invalid amount";
                return RedirectToAction("Index", "UserDashboard");
            }

            if (string.IsNullOrWhiteSpace(model.Currency))
            {
                TempData["Error"] = "Select currency";
                return RedirectToAction("Index", "UserDashboard");
            }

            // ================= SAVE PAYMENT =================
            model.UserId = userId.Value;
            model.Status = "Pending";
            model.DateCreated = DateTime.Now;

            _context.Payments.Add(model);
            _context.SaveChanges();

            // ================= FIX: TempData (NO DECIMAL ERROR) =================
            TempData["Recipient"] = model.RecipientName ?? "";
            TempData["Amount"] = model.Amount.ToString("0.00"); // ✅ FIXED
            TempData["Bank"] = model.BankName ?? "";
            TempData["Currency"] = model.Currency ?? "";
            TempData["Status"] = "Pending";

            // ================= REDIRECT =================
            return RedirectToAction("Success");
        }

        // ================= SUCCESS PAGE =================
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}