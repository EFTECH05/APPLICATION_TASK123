using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;
using System.Diagnostics;

namespace PaymentSystem.Controllers
{
    // This controller handles main pages (Home, Privacy, Services, Contact)
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ================= HOME PAGE =================
        // URL: /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // ================= PRIVACY PAGE =================
        // URL: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // ================= SERVICES PAGE =================
        // URL: /Home/Services
        public IActionResult Services()
        {
            return View();
        }

        // ================= CONTACT PAGE =================
        // URL: /Home/Contact
        // This makes your "Contact" navbar link work
        public IActionResult Contact()
        {
            return View(); // Loads Views/Home/Contact.cshtml
        }

        // ================= ERROR PAGE =================
        // Used when something goes wrong
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}