using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Invest_Site_3._0.Models;
using System.Web.Script.Services;
using System.Web.Services;


namespace Invest_Site_3._0.Controllers
{
    public class HomeController : Controller
    {

        List<History_Daily> lastDays = new List<History_Daily>();
        List<History_Daily> TLTDays = new List<History_Daily>();

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
    }
}