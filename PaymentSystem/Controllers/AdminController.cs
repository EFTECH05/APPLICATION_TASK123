using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ================= ROLE CHECK =================
        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("Role");
            return role == "Admin" || role == "SuperAdmin";
        }

        private bool IsSuperAdmin()
        {
            var role = HttpContext.Session.GetString("Role");
            return role == "SuperAdmin";
        }

        private IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Auth");
        }

        // ================= DASHBOARD =================
        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var users = _context.Users
                .AsNoTracking()
                .ToList();

            var payments = _context.Payments
                .AsNoTracking()
                .ToList();

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
            if (!IsAdmin())
                return RedirectToLogin();

            var users = _context.Users
                .AsNoTracking()
                .ToList();

            return View(users);
        }

        // ================= CREATE USER =================
        [HttpGet]
        public IActionResult CreateUser()
        {
            if (!IsAdmin())
                return RedirectToLogin();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(User model)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            if (!ModelState.IsValid)
                return View(model);

            model.Role = "User";

            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= CREATE ADMIN =================
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            if (!IsAdmin())
                return RedirectToLogin();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdmin(User model)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            if (!ModelState.IsValid)
                return View(model);

            model.Role = "Admin";
            model.Status = "Pending";
            model.IsApproved = false;
            model.CreatedBy = HttpContext.Session.GetString("User");

            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= REQUEST UPDATE (GET) =================
        [HttpGet]
        public IActionResult RequestUpdate(int id)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            if (user.Role == "SuperAdmin")
            {
                TempData["Error"] = "Main Admin cannot be edited.";
                return RedirectToAction("Users");
            }

            return View(user);
        }

        // ================= REQUEST UPDATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestUpdate(User model)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var user = _context.Users.Find(model.Id);

            if (user == null)
                return NotFound();

            if (user.Role == "SuperAdmin")
                return RedirectToAction("Users");

            // BANKING STYLE: NO DIRECT UPDATE, ONLY REQUEST STATUS
            user.Name = model.Name;
            user.Email = model.Email;
            user.Status = "Pending Update Approval";

            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= UPDATE REQUEST LIST (SUPERADMIN) =================
        public IActionResult UpdateRequests()
        {
            if (!IsSuperAdmin())
                return RedirectToLogin();

            var requests = _context.Users
                .AsNoTracking()
                .Where(x => x.Status == "Pending Update Approval")
                .ToList();

            return View(requests);
        }

        // ================= APPROVE UPDATE =================
        public IActionResult ApproveUpdate(int id)
        {
            if (!IsSuperAdmin())
                return RedirectToLogin();

            var user = _context.Users.Find(id);

            if (user != null)
            {
                user.Status = "Active";
                _context.SaveChanges();
            }

            return RedirectToAction("UpdateRequests");
        }

        // ================= DELETE USER =================
        public IActionResult DeleteUser(int id)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var user = _context.Users.Find(id);

            if (user != null)
            {
                if (user.Role == "SuperAdmin")
                {
                    TempData["Error"] = "Main Admin cannot be deleted.";
                    return RedirectToAction("Users");
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Users");
        }
    }
}