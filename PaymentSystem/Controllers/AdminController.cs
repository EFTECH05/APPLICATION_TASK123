using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Models;
using PaymentSystem.Filters;

namespace PaymentSystem.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ================= ROLE =================
        private string? GetRole()
        {
            return HttpContext.Session.GetString("Role");
        }

        // ================= DASHBOARD =================
        public IActionResult Index()
        {
            var users = _context.Users.AsNoTracking().ToList();
            var payments = _context.Payments.AsNoTracking().ToList();

            var model = new DashboardViewModel
            {
                TotalUsers = users.Count,
                TotalPayments = payments.Count,
                TotalRevenue = payments.Sum(p => (decimal?)p.Amount) ?? 0,
                Users = users
            };

            return View(model);
        }

        // ================= USERS =================
        public IActionResult Users()
        {
            var role = GetRole();

            IQueryable<User> users = _context.Users.AsNoTracking();

            if (role == "Admin")
            {
                users = users.Where(u => u.Role == "User");
            }

            return View(users.ToList());
        }

        // ================= CREATE USER =================
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Role = "User";
            model.CreatedAt = DateTime.Now;

            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= CREATE ADMIN (FIXED SECURITY) =================
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            var role = GetRole();

            // 🚫 ONLY SUPER ADMIN CAN ACCESS
            if (role != "SuperAdmin")
                return View("AccessDenied");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdmin(User model)
        {
            var role = GetRole();

            // 🚫 BLOCK NON SUPER ADMIN
            if (role != "SuperAdmin")
                return View("AccessDenied");

            if (!ModelState.IsValid)
                return View(model);

            model.Role = "Admin";
            model.Status = "Pending";
            model.IsApproved = false;
            model.CreatedBy = HttpContext.Session.GetString("User");
            model.CreatedAt = DateTime.Now;

            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= REQUEST UPDATE (GET) =================
        [HttpGet]
        public IActionResult RequestUpdate(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            if (user.Role == "SuperAdmin")
                return RedirectToAction("Users");

            return View(user);
        }

        // ================= REQUEST UPDATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestUpdate(User model)
        {
            var user = _context.Users.Find(model.Id);

            if (user == null)
                return NotFound();

            if (user.Role == "SuperAdmin")
                return RedirectToAction("Users");

            user.PendingName = model.Name;
            user.PendingEmail = model.Email;
            user.Status = "Pending Update Approval";

            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= UPDATE REQUESTS =================
        public IActionResult UpdateRequests()
        {
            var role = GetRole();

            if (role != "SuperAdmin")
                return RedirectToAction("Users");

            var requests = _context.Users
                .AsNoTracking()
                .Where(x => x.Status == "Pending Update Approval")
                .ToList();

            return View(requests);
        }

        // ================= APPROVE UPDATE =================
        public IActionResult ApproveUpdate(int id)
        {
            var role = GetRole();

            if (role != "SuperAdmin")
                return RedirectToAction("Users");

            var user = _context.Users.Find(id);

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.PendingName))
                    user.Name = user.PendingName;

                if (!string.IsNullOrEmpty(user.PendingEmail))
                    user.Email = user.PendingEmail;

                user.Status = "Active";

                _context.SaveChanges();
            }

            return RedirectToAction("UpdateRequests");
        }

        // ================= DELETE USER =================
        public IActionResult DeleteUser(int id)
        {
            var role = GetRole();

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            if (user.Role == "SuperAdmin")
                return RedirectToAction("Users");

            if (role == "Admin" && user.Role != "User")
                return RedirectToAction("Users");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }
    }
}