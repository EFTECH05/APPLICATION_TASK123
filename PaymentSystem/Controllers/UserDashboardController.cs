using Microsoft.AspNetCore.Mvc;

namespace PaymentSystem.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}