using java.util;
using Project_TL.Models.Domain;
using Project_TL.ViewModels.Report;
using Project_TL.Views.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class ReportController : Controller
    {
        private IHotelRepository hotelRepo;
        private  List<SecondMakeReport> list;
        // GET: Report

        public ReportController(IHotelRepository hotelRepo)
        {
            this.hotelRepo = hotelRepo;
            list = new List<SecondMakeReport>();
        }
        public ReportController()
        {

        }
        public ActionResult Index()
        {
            IEnumerable<MakeReport> list = hotelRepo.FindAll().ToList().Select(t => new MakeReport(t));
            ReportViewModel rvm = new ReportViewModel(list);
            return View(rvm);
        }
        [HttpPost]
        public ActionResult Index(ReportViewModel model)
        {
            try
            {
                List<Hotel> listChecked = new List<Hotel>();
                model.List.ForEach(t =>
                {
                    if (t.Checked)
                    {
                        listChecked.Add(hotelRepo.FindByCode(t.HotelId));
                    }
                });

                list = listChecked.Select(t => new SecondMakeReport(t)).ToList();
                TempData["list"] = list;


                if (DateTime.Compare(model.EndDate, DateTime.Today) > 0)
                {

                    return RedirectToAction("ConfirmationPage",new { l = list, future = true });
                }
                else
                {
                    return RedirectToAction("ConfirmationPage", new { l = list, future = false });
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

        }
        public ActionResult ConfirmationPage(List<SecondMakeReport> l, bool future)
        {
            l = list;
            l = (List <SecondMakeReport> )TempData["list"];
            SecondReportViewModel srvm = new SecondReportViewModel(l, future);
            return View(srvm);

        }

        public ActionResult ReportPage()
        {

        }

        public ActionResult Chart()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            new Chart(width: 6060, height: 400, theme: ChartTheme.Blue).AddTitle("Costs").AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }


    }
}

