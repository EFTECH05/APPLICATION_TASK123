using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly AppDbContext _context;

        public UserDashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 🔐 Get logged-in user email from session
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Auth");
            }

            // 🔍 Fetch user from database
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // ✅ Send user to view
            return View(user);
        }
    }
}