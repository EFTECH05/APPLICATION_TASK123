using Microsoft.AspNetCore.Mvc;

namespace FirstMVCproject.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult DeleteProduct()
        {
            return View();
        }

        public ActionResult UpdateProduct(int ID)
        {
            return View();
        }
    }
}

