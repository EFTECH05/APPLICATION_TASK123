using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Controllers
{
    public class AuthController : Controller
    {
        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // DEMO LOGIN (replace with DB later)
            if (username == "admin" && password == "123")
            {
                // ✅ STORE USER SESSION
                HttpContext.Session.SetString("User", username);

                return RedirectToAction("Index", "UserDashboard");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        // ================= REGISTER =================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }

            // TODO: Save user to database later

            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}