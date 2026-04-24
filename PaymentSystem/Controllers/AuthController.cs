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

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View(model);
            }

            var user = new User
            {
                Name = model.FullName,
                Email = model.Email,
                Password = model.Password,
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

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
            var user = _context.Users
                .FirstOrDefault(x =>
                    (x.Email == username || x.Name == username)
                    && x.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid login details";
                return View();
            }

            // ================= SESSION (FIXED) =================
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("User", user.Name);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Email", user.Email);

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