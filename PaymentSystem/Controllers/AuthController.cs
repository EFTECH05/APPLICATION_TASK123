using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ================= BASIC VALIDATION =================
            if (string.IsNullOrWhiteSpace(model.FullName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.Error = "All fields are required";
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View(model);
            }

            // ================= CHECK DUPLICATE EMAIL =================
            var existingUser = _context.Users
                .FirstOrDefault(x => x.Email == model.Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email already exists";
                return View(model);
            }

            // ================= CREATE USER =================
            var user = new User
            {
                Name = model.FullName.Trim(),
                Email = model.Email.Trim(),
                Password = model.Password, // (you will upgrade to hashing later for Task 3)
                Role = "User"
            };

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // 🔥 REAL DEBUG (THIS IS IMPORTANT)
                ViewBag.Error =
                    "MESSAGE: " + ex.Message +
                    " | INNER: " + ex.InnerException?.Message;

                return View(model);
            }

            return RedirectToAction("Login");
        }

        // ================= LOGIN =================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required";
                return View();
            }

            var user = _context.Users
                .FirstOrDefault(x =>
                    (x.Email == username || x.Name == username)
                    && x.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid login details";
                return View();
            }

            // ================= SESSION =================
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("User", user.Name ?? "User");
            HttpContext.Session.SetString("Role", user.Role ?? "User");
            HttpContext.Session.SetString("Email", user.Email ?? "");

            // ================= ROLE REDIRECT =================
            if (user.Role == "SuperAdmin")
                return RedirectToAction("Index", "Admin");

            if (user.Role == "Admin")
                return RedirectToAction("Index", "Admin");

            return RedirectToAction("Index", "UserDashboard");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}