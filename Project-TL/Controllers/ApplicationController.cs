using Project_TL.Models.DAL;
using Project_TL.Models.Domain;
using Project_TL.ViewModels.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class ApplicationController : Controller
    {
        private IApplicationRepository systRepo;
        private IHotelRepository hotelRepo;
        private List<Hotel> hotels;
        public ApplicationController()
        {

        }
        public ApplicationController(IApplicationRepository systRepo, IHotelRepository hotelRepo)
        {
            this.systRepo = systRepo;
            this.hotelRepo = hotelRepo;
            hotels = new List<Hotel>();
        }
        // GET: Application
        public ActionResult Index()
        {
            IEnumerable<ApplicationViewModel> list = systRepo.FindAll().ToList().Select(t => new ApplicationViewModel(t));

            return View(list);
        }

        //CRUD methods Hotel

        public ActionResult Details(int id)
        {
            Application s = systRepo.FindById(id);
            ApplicationViewModel avw = new ApplicationViewModel(s);
            if (Request.IsAjaxRequest())
                return PartialView("DetailsList", avw);
            return View(avw);
        }


        public ActionResult Create()
        {
            //making the list of status

            List<Models.Domain.ApplicationType> type = new List<Models.Domain.ApplicationType>();
            foreach (Models.Domain.ApplicationType t in Enum.GetValues(typeof(Models.Domain.ApplicationType)))
            {
                type.Add(t);
            }

            Application a = new Application();
            EditApplicationViewModel avm = new EditApplicationViewModel(a)
            {
                SelectedStatusId = type.FirstOrDefault().ToString(),
                TypeList = type.Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString()
                }),
            };
            
            return View(avm);
        }

        [HttpPost]
        public ActionResult Create(EditApplicationViewModel model)
        {
           
            try
            {
                Application ap = new Application();
                systRepo.AddSyst(ap);
                MapToApplication(model, ap);

                systRepo.SaveChanges();
                TempData["message"] = String.Format("Application {0} has been created", ap.Name);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            List<Models.Domain.ApplicationType> type = new List<Models.Domain.ApplicationType>();
            foreach (Models.Domain.ApplicationType t in Enum.GetValues(typeof(Models.Domain.ApplicationType)))
            {
                type.Add(t);
            }
            model.TypeList = type.Select(t => new SelectListItem
            {
                Value = t.ToString(),
                Text = t.ToString()
            });
            return View("Create", model);

        }

       public ActionResult Edit(int id)
        {
            Application ap = systRepo.FindById(id);
            List<ApplicationType> type = new List<ApplicationType>();

            foreach (ApplicationType t in Enum.GetValues(typeof(ApplicationType)))
            {
                type.Add(t);
            }
            //check if the application exist (normally this can never give null unless DB failure)
            if (ap == null)
            {
                return HttpNotFound();
            }

            //fill the dropdown and select the default type
            EditApplicationViewModel eavm = new EditApplicationViewModel(ap)
            {
                SelectedStatusId = type.FirstOrDefault().ToString(),
                TypeList = type.Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString()
                })

            };

            return View(eavm);

        }
        [HttpPost]
        public ActionResult Edit(int id,EditApplicationViewModel model)
        {
            try
            {
                Application ap = systRepo.FindById(id);
                MapToApplication(model, ap);
                systRepo.SaveChanges();
                TempData["message"] = String.Format("Application {0} werd aangepast", ap.Name);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            // http policy requires to fill the list again after the post
            List<ApplicationType> type = new List<ApplicationType>();

            foreach (ApplicationType t in Enum.GetValues(typeof(ApplicationType)))
            {
                type.Add(t);
            }
            model.TypeList = type.Select(t => new SelectListItem
            {
                Value = t.ToString(),
                Text = t.ToString()
            });
            return Edit(id);
        }

        public ActionResult Delete(int id)
        {
            Application s = systRepo.FindById(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Application s = systRepo.FindById(id);
                if (s == null)
                    return HttpNotFound();

                //error when you try to use the hotelrepo
                List<Hotel> hot = new List<Hotel>();
                systRepo.getContext().Hotels.ToList().ForEach(t =>
            {
                t.Applications.ToList().ForEach(r =>
                {
                    if (r.ApplicationId == s.ApplicationId)
                        hot.Add(t);
                });
            });
                    

                //check all hotels and delete the application if they have it
                foreach (Hotel h in hot)
                {
                    h.Applications.ToList().RemoveAll(t => t.ApplicationId == id && t.HotelId == h.HotelId);
                   
                }

                //save everything and popup message if it's all ok
                systRepo.RemoveSyst(s);
                systRepo.SaveChanges();
              
                TempData["message"] = String.Format("Application {0} was succesfully deleted", s.Name);

            }
            catch (Exception ex)
            {
                TempData["error"] = "There was an error when deleting the hotel from the applciation. Please contact the IT department.";
            }
            return RedirectToAction("Index");
        }


        //hotels of the application crud methods

        public ActionResult AddHotel(int id)
        {
            Application s = systRepo.FindById(id);
            List<int> i = new List<int>();
            hotels = hotelRepo.FindAll().ToList();

            //deleting the hotels that the application already has
            //You can't delete a hotel while in the loop so first save the positions and after that loop it and delete the hotels
            foreach (Hotel h in hotels)
            {               
                h.Applications.ToList().ForEach(t =>
               {

                   if (t.ApplicationId == id && DateTime.Compare(t.EndDate,DateTime.Today) > 0)
                       i.Add(hotels.IndexOf(h));
               });
            }

            //after deleting a hotel the indexes change? Use 'j' to solve this

            for (int j = 0; j < i.Count(); j++)
            {

                hotels.RemoveAt(i.ElementAt(j) - j);
            }

            IEnumerable<AddHotel> list = hotels.Select(t => new AddHotel(t));
            AddHotelToApplicationViewModel model = new AddHotelToApplicationViewModel(list.ToList());
            return View(model);

        }
        [HttpPost, ActionName("addHotel")]
        public ActionResult addHotelConfirmed(int id, AddHotelToApplicationViewModel model)
        {
            Application s = systRepo.FindById(id);
            if (s == null)
                return HttpNotFound();
            try
            {
                for (int i = 0; i < model.Hotels.Count(); i++)
                {
                    if (model.Hotels[i].Checked == true)
                    {

                        Hotel h = hotelRepo.FindByCode(model.Hotels[i].HotelId);
                        HotelApplication ha = new HotelApplication();

                        //fill up the object HA from the model etc
                        MapToApplication(ha, model.Hotels[i], s,h);

                        s.addHotel(ha);                    
                        h.addApplication(ha);

                        hotelRepo.SaveChanges();
                        TempData["message"] = String.Format("Hotel {0} was added to the list", h.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "There was a problem when trying to add the hotel. Try again later. If this keeps happening, please contact the IT administrator";
                //if the hotel was added but the error came from the DB
                //s.removeHotel(hotelRepo.FindByCode(hotelId).Applications.Where(t => t.ApplicationId == id).First());
                
                return AddHotel(id);

            }
            return RedirectToAction("Details", new { id = id });


        }
        public ActionResult DeleteHotel(string id, int systId,DateTime endDate)
        {
            try
            {
                Hotel h = hotelRepo.FindByCode(id);
                Application s = systRepo.FindById(systId);
                h.removeApplication(h.Applications.Where(t => t.ApplicationId == systId).Where(t => t.HotelId == id).FirstOrDefault());
                s.removeHotel(s.Hotels.Where(t => t.ApplicationId == systId).Where(t => t.HotelId == id).Where(t => t.EndDate.Equals(endDate)).FirstOrDefault());

                systRepo.SaveChanges();
                
                TempData["message"] = "The system has been deleted from the hotel";
            }
            catch (Exception ex)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
           
            return RedirectToAction("Details", new { id = systId });
        }

        private void MapToApplication(HotelApplication ha, AddHotel model, Application s,Hotel h)
        {
            ha.ApplicationId = s.ApplicationId;
            ha.HotelId = h.HotelId;
            ha.Cost = model.Cost;
            ha.ApplicationName = s.Name;
            ha.HotelName = h.Name;
            
            ha.StartDate = model.StartDate;
            ha.EndDate = model.EndDate;

            Maintenance m = new Maintenance(model.MStartDate, model.MEndDate, model.MaintenanceCost);
            ha.Maintenance = m;
            //s1.Type = s.Type;
        }
        private void MapToApplication(EditApplicationViewModel model, Application ap)
        {
            ap.Name = model.Name;
            ap.Type = getEnum(model.SelectedStatusId);
            ap.ApplicationId = model.Id;
            
           
        }

        private Models.Domain.ApplicationType getEnum(string selectedStatusId)
        {
            switch(selectedStatusId)
            {
                case "Leased": return Models.Domain.ApplicationType.Leased;
                case "Rented": return Models.Domain.ApplicationType.Rented;
                case "Purashed":return Models.Domain.ApplicationType.Purashed;
                default: return Models.Domain.ApplicationType.Leased;
            }
        }
    }

}