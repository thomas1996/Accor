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
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

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

                    return RedirectToAction("ConfirmationPage",new { l = list, future = true,startDate = model.StartDate,endDate = model.EndDate });
                }
                else
                {
                    return RedirectToAction("ConfirmationPage", new { l = list, future = false, startDate = model.StartDate, endDate = model.EndDate });
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

        }
        public ActionResult ConfirmationPage(List<SecondMakeReport> l, bool future,DateTime startDate,DateTime endDate)
        {
           
            l = (List <SecondMakeReport> )TempData["list"];           
            SecondReportViewModel srvm = new SecondReportViewModel(l, future,startDate,endDate);
            
            return View(srvm);

        }

        [HttpPost,ActionName("ConfirmationPage")]
        public ActionResult ConfirmationPageBis(SecondReportViewModel model)
        {
            try
            {
                System.Data.DataTable data =  MakeTable(model.List);
                MakeExcel(data);


                return RedirectToAction("Index");
                TempData["model"] = model;
                //return RedirectToAction("Index");

            }catch(Exception ex)
            {
                TempData["error"] = "there has been an error. Please contact the IT department";
            }
            return RedirectToAction("ReportPage");
        }

       
        
        public ActionResult ReportPage()
        {
            SecondReportViewModel model = (SecondReportViewModel) TempData["model"];
            return View(model); 
        }

        private System.Data.DataTable MakeTable(List<SecondMakeReport> list)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            PropertyInfo[] prop = typeof(SecondMakeReport).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pr in prop)
            {
                table.Columns.Add(pr.Name);
            }


            foreach(SecondMakeReport item in list)
            {
                var values = new object[prop.Length];
                for (int i = 0; i < prop.Length; i++)
                {
                    values[i] = prop[i].GetValue(item, null);
                }
                table.Rows.Add(values);
            };
            
            return table;
        }
        private void MakeExcel(DataTable data)
        {
            string FileName = "report" + DateTime.Today.ToShortDateString();
            FileInfo f = new FileInfo(Server.MapPath("Downloads") + string.Format("\\{0}.xlsx", FileName));
            if (f.Exists)
                f.Delete();
            // delete the file if it already exist.

            HttpResponseBase response = HttpContext.Response;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.Charset = Encoding.UTF8.WebName;
            response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            response.AddHeader("Content-Type", "application/Excel");
            response.ContentType = "application/vnd.xlsx";
            //response.AddHeader("Content-Length", file.Length.ToString());
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");


            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = data;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    dg.Dispose();
                    data.Dispose();
                    response.End();
                }
            }
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

