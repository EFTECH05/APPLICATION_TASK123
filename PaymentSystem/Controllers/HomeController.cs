using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;
using System.Diagnostics;

namespace PaymentSystem.Controllers
{
<<<<<<< HEAD
    // This controller handles main pages (Home, Privacy, Services, Contact)
=======
>>>>>>> a298f210f7d589ca76a3a74262b11ebc40b0c253
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

<<<<<<< HEAD
        // ================= HOME PAGE =================
        // URL: /Home/Index
=======
>>>>>>> a298f210f7d589ca76a3a74262b11ebc40b0c253
        public IActionResult Index()
        {
            return View();
        }

<<<<<<< HEAD
        // ================= PRIVACY PAGE =================
        // URL: /Home/Privacy
=======
>>>>>>> a298f210f7d589ca76a3a74262b11ebc40b0c253
        public IActionResult Privacy()
        {
            return View();
        }

<<<<<<< HEAD
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
=======
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
>>>>>>> a298f210f7d589ca76a3a74262b11ebc40b0c253
