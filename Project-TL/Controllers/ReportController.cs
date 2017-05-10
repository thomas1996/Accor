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
using Microsoft.Office.Interop.Excel;

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

                    return RedirectToAction("ConfirmationPage",new { future = true});
                }
                else
                {
                    return RedirectToAction("ConfirmationPage", new { future = false});
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

        }
        public ActionResult ConfirmationPage(bool future)
        {
           
           List<SecondMakeReport> l = (List <SecondMakeReport> )TempData["list"];
            DateTime startDate = (DateTime) TempData["Start"];
            DateTime endDate = (DateTime)TempData["End"];   
            SecondReportViewModel srvm = new SecondReportViewModel(l, future,startDate,endDate);
            
            return View(srvm);

        }

        [HttpPost,ActionName("ConfirmationPage")]
        public ActionResult ConfirmationPageBis(SecondReportViewModel model)
        {
            try
            {
                //System.Data.DataTable data =  MakeTable(model.List);
                //MakeExcel(data);

                TempData["model"] = model;
                return RedirectToAction("ReportPage");

            }
            catch(Exception ex)
            {
                TempData["error"] = "there has been an error. Please contact the IT department";
                return RedirectToAction("Index");
            }
           
        }

       
        
        public ActionResult ReportPage()
        {
            SecondReportViewModel model = (SecondReportViewModel) TempData["model"];
            TempData["model"] = model;

            model.BranchList = model.List.GroupBy(t => t.BranchName, t => t.HAList).ToList();
            double cost = 0.0;
            model.BranchList.ForEach(t =>
        {
            t.ToList().ForEach(s =>
            {
                s.ForEach(r =>
               {
                   cost += r.Cost;
               });
            });
            model.BranchCost.Add(cost);
            cost = 0.0;
        });
            
            return View(model); 
        }

        [HttpPost,ActionName("ReportPage")]
        public ActionResult ReportPageConfirmed()
        {
            try
            {
                SecondReportViewModel model = (SecondReportViewModel)TempData["model"];
                System.Data.DataTable data = MakeTable(model.List);
                MakeExcel(data);

            }
            catch (Exception ex)
            {
                TempData["error"] = "there has been an error. Please contact the IT department";
                return RedirectToAction("Index");
            }
            return Redirect("ReportPage");
        }

        private System.Data.DataTable MakeTable(List<SecondMakeReport> list)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            //Get all the properties of SecondMakeReport for the titles
            PropertyInfo[] prop = typeof(SecondMakeReport).GetProperties(BindingFlags.Public | BindingFlags.Instance);
           
            for(int i = 0; i < 8; i ++)
            {
                switch(i)
                {
                    case 0: case 1: case 2: case 3: table.Columns.Add(prop[i].Name); break;
                    case 4: table.Columns.Add("Application"); break;
                    case 5: table.Columns.Add("Start date - End date"); break;
                    case 6: table.Columns.Add("Cost price"); break;
                    case 7: table.Columns.Add("Future cost price (after end date)"); break;
                }
            }


            foreach(SecondMakeReport item in list)
            {
                //Make it +2 because in the begin we have a switch untill 7, we added 2 extra columns
                var values = new object[prop.Length + 2];
                for (int i = 0; i < prop.Length + 2 ; i++)
                {
                   if(i == 4)
                    {
                        StringBuilder sb = new StringBuilder();
                        item.HAList.ForEach(t =>
                        {
                            String s = t.ApplicationName + "<br/>";
                            sb.Append(s);

                        });
                        values[i] = sb.ToString();
                    } else if (i == 5)
                    {
                        StringBuilder sb = new StringBuilder();
                        item.HAList.ForEach(t =>
                        {
                            String s = t.StartDate.ToShortDateString() + " - " + t.EndDate.ToShortDateString() + "<br/>";
                            sb.Append(s);
                        });
                        values[i] = sb.ToString();
                    }else if(i == 6)
                    {
                        StringBuilder sb = new StringBuilder();
                        item.HAList.ForEach(t =>
                        {
                            String s = t.Cost.ToString() + "<br/>";
                            sb.Append(s);
                        });
                        values[i] = sb.ToString();
                    }else if (i == 7)
                    {
                        StringBuilder sb = new StringBuilder();
                        item.NewListCost.ForEach(t =>
                        {
                            if (t > 0)
                            {
                                String s = t.ToString() + "<br/>";
                                sb.Append(s);
                            }
                            else
                            {
                                String s = " " + "<br/>";
                                sb.Append(s);
                            }
                        });
                        values[i] = sb.ToString();
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        item.HAList.ForEach(t =>
                        {
                            String s = prop[i].GetValue(item, null).ToString() + "<br/>";
                            sb.Append(s);
                        });
                        values[i] = sb.ToString();
                    }
                    
                }
                table.Rows.Add(values);
            };
            
            return table;
        }
        private void MakeExcel(System.Data.DataTable data)
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
                    response.SetCookie(new HttpCookie("fileDownload", "true") { Path = "/" });
                    //response.Redirect("ReportPage");
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


            

            new System.Web.Helpers.Chart(width: 6060, height: 400, theme: ChartTheme.Blue).AddTitle("Costs").AddSeries("Default", chartType: "line", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }


    }
}

