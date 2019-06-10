using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContractManager3.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AdminView()
        {
            ViewBag.Message = "Welcome Administrator";

            return View();
        }

        public ActionResult SupplierView()
        {
            ViewBag.Message = "Welcome Supplier";

            return View();
        }

        public ActionResult DeptView()
        {
            ViewBag.Message = "Welcome Dept" + User.ToString();

            return View();
        }
    }
}