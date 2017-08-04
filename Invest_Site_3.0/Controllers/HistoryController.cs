using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Globalization;

using Invest_Site_3._0.Models;

namespace Invest_Site_3._0.Controllers
{
    public class HistoryController : Controller
    {
        List<History_Daily> lastDays = new List<History_Daily>();
        List<History_Daily> TLTDays = new List<History_Daily>();
        List<History_Daily> QQQDays = new List<History_Daily>();
        List<History_Daily> TQQQDays = new List<History_Daily>();

        // GET: History
        public ActionResult Index()
        {
            using (INVESTEntities histDaily = new INVESTEntities())
            {
                lastDays = histDaily.History_Daily.OrderByDescending(p => p.Date).ToList();

                TLTDays = histDaily.History_Daily.Where(a => a.Tiker.Contains("TLT"))
                                                .OrderBy(item => item.Date).ToList();

                QQQDays = histDaily.History_Daily.Where(a => a.Tiker == "QQQ")
                                                .OrderBy(item => item.Date).ToList();

                TQQQDays = histDaily.History_Daily.Where(a => a.Tiker.Contains("TQQQ"))
                                                 .OrderBy(item => item.Date).ToList();

            }

            // Find Current PL and Date:

            var MaxDate = (from d in lastDays select d.Date).Max();
            var CurrentResult = lastDays.Where(a => a.Date == MaxDate);
            var CurrentPL = CurrentResult.Sum(a => a.PL_Cum).ToString();

            ViewBag.PL = CurrentPL;
            ViewBag.LastDate = MaxDate.ToString("d");


            //Chart 

            var TLTValues = TLTDays.Select(t => new { t.PL_Cum, t.Date }).ToList();
            var QQQValues = QQQDays.Select(t => new { t.PL_Cum, t.Date }).ToList();
            var TQQQValues = TQQQDays.Select(t => new { t.PL_Cum, t.Date }).ToList();


            //List<LineSeriesData> Days = new List<LineSeriesData>();
            //List<LineSeriesData> TLTData = new List<LineSeriesData>();
            //List<LineSeriesData> QQQData = new List<LineSeriesData>();
            //List<LineSeriesData> TQQQData = new List<LineSeriesData>();

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            ////AllDays.ForEach(p => Days.Add(new LineSeriesData {X = DateTime.Parse(p.Ticks)}));
            //TLTValues.ForEach(p => TLTData.Add(new LineSeriesData { Y = p.PL_Cum, X = (p.Date - epoch).TotalMilliseconds }));
            //QQQValues.ForEach(p => QQQData.Add(new LineSeriesData { Y = p.PL_Cum, X = (p.Date - epoch).TotalMilliseconds }));
            //TQQQValues.ForEach(p => TQQQData.Add(new LineSeriesData { Y = p.PL_Cum, X = (p.Date - epoch).TotalMilliseconds }));

            //ViewData["TLT"] = TLTData;
            //ViewData["QQQ"] = QQQData;
            //ViewData["TQQQ"] = TQQQData;
            //ViewData["Days"] = Days;


            // return View(lastDays);
            return View("~/Views/Account/Register.cshtml");
        }

        public JsonResult GetTotal_PL()
        {

            List<History_Daily> PL_Days = new List<History_Daily>();

            using (INVESTEntities histDaily = new INVESTEntities())
            {
                PL_Days = histDaily.History_Daily.OrderByDescending(p => p.Date).ToList();
            }
           
            var PL = PL_Days.Select(t => new { t.PL_Cum, t.Date }).ToList();
            // var PL1 = PL.GroupBy(l => l.Date).Select(t=>t.Sum(s=>s.PL_Cum)).ToList();

            

            var PL1 =
                from dr in PL
                group dr by dr.Date into plGroup
                select new
                {
                    Date = plGroup.Key,
                    TotalPL = plGroup.Sum(x => x.PL_Cum),
                };

           

            //8.15
            return new JsonResult {Data = PL1, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        
        public JsonResult GetAllDays()
        {
            List<History_Daily> allDays = new List<History_Daily>();

            using (INVESTEntities histDaily = new INVESTEntities())
            {
                allDays = histDaily.History_Daily.ToList();
            }

            //8.15
            return new JsonResult { Data = allDays, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetAllDaysWithParameters(string prefix)
        {
            List<History_Daily> allDays = new List<History_Daily>();

            using (INVESTEntities histDaily = new INVESTEntities())
            {
                allDays = histDaily.History_Daily.Where(a => a.Tiker.Contains(prefix)).ToList();
            }

            //8.15
            return new JsonResult { Data = allDays, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
     
    }

   
}