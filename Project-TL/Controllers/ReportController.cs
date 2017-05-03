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
using NsExcel = Microsoft.Office.Interop.Excel;

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
                TempData["Start"] = model.StartDate;
                TempData["End"] = model.EndDate;


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
           
            l = (List <SecondMakeReport> )TempData["list"];           
            SecondReportViewModel srvm = new SecondReportViewModel(l, future);
            
            return View(srvm);

        }

        [HttpPost,ActionName("ConfirmationPage")]
        public ActionResult ConfirmationPageBis(SecondReportViewModel model)
        {
            try
            {
                MakeTable(model.List);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        public ActionResult ReportPage()
        {
            

            return View(); 
        }

        private System.Data.DataTable MakeTable(List<SecondMakeReport> list)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add();
            list.ForEach(t =>
            {
                var row = table.NewRow();
                row[0] = t.OwnerName;
                table.Rows.Add(row);
            });

            return table;
        }

        public ActionResult Chart()
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            DateTime start = (DateTime)TempData["Start"];
            DateTime end = (DateTime)TempData["End"];


            

            new Chart(width: 6060, height: 400, theme: ChartTheme.Blue).AddTitle("Costs").AddSeries("Default", chartType: "line", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }


    }
}

