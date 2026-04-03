using Microsoft.AspNetCore.Mvc;

namespace FirstMVCproject.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployee()
        {
            return View();
        }
        public ActionResult UpdateEmployee()
        {
            return View();
        }
    }
}
