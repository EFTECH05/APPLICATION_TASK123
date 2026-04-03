using Microsoft.AspNetCore.Mvc;

namespace FirstMVCproject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Looks for Views/Home/Index.cshtml
        }

        public IActionResult FirstAction()
        {
            return View(); // Looks for Views/Home/FirstAction.cshtml
        }

        public IActionResult GotoProduct()
        {
            // Redirects to Product/AddProduct
            return RedirectToAction("AddProduct", "Product");
        }
    }
}

