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
        private ISystemRepository systRepo;
        private IHotelRepository hotelRepo;
        public ApplicationController()
        {

        }
        public ApplicationController(ISystemRepository systRepo,IHotelRepository hotelRepo)
        {
            this.systRepo = systRepo;
            this.hotelRepo = hotelRepo;
        }
        // GET: Application
        public ActionResult Index()
        {
            IEnumerable<ApplicationViewModel> list = systRepo.FindAll().ToList().Select(t => new ApplicationViewModel(t));
            
            return View(list);
        }
       
        public ActionResult Details(int id)
        {
           Syst s = systRepo.FindById(id);
           ApplicationViewModel avw = new ApplicationViewModel(s);
            if (Request.IsAjaxRequest())
                return PartialView("DetailsList", avw);
                return View(avw);
        }
        
        public ActionResult DeleteApplication(string id,int systId)
        {
            try
            {
                Hotel h = hotelRepo.FindByCode(id);
                Syst s = systRepo.FindById(systId);
                h.removeApplication(s);
                s.removeHotel(h);

                //systRepo.EditSyst(s);
                //hotelRepo.EditHotel(h);

                systRepo.SaveChanges();
                hotelRepo.SaveChanges();
                TempData["message"] = String.Format("The system has been deleted from the hotel");
            }
            catch(Exception ex)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            if (Request.IsAjaxRequest())
                return Details(systId);
                return RedirectToAction("Details", new { id = systId });
        }

        public ActionResult Delete(int id)
        {
            Syst s = systRepo.FindById(id);
            if(s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                Syst s = systRepo.FindById(id);
                if (s == null)
                    return HttpNotFound();
                List<Hotel> hot = hotelRepo.FindBySystem(s.Name).ToList();

                //check all hotels and delete the application if they have it
                foreach (Hotel h in hot)
                {
                    h.removeApplication(s);
                }

                //save everything and popup message if it's all ok
                systRepo.RemoveSyst(s);
                systRepo.SaveChanges();
                hotelRepo.SaveChanges();
                TempData["message"] = String.Format("Application {0} was succesfully deleted",s.Name);

            }
            catch(Exception ex)
            {
                TempData["error"] = "There was an error, if this keeps happening, please contact the IT administrator";
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddHotel(int id)
        {
            Syst s = systRepo.FindById(id);
            List<int> i = new List<int>();
            List<Hotel> hotels = hotelRepo.FindAll().ToList();
            
            //deleting the hotels that the application already has
            //You can't delete a hotel while in the loop so first save the positions and after that loop it and delete the hotels
            foreach(Hotel h in s.Hotels)
            {
                hotels.ForEach(t =>
               {
                  
                   if (t.HotelId == h.HotelId)
                       i.Add(hotels.IndexOf(t));
               });
            }

            //after deleting a hotel the indexes change? Use 'j' to solve this

            for(int j = 0; j<i.Count();j++)
            {
                
                hotels.RemoveAt(i.ElementAt(j) - j);
            }

            IEnumerable<AddHotelToApplicationViewModel> list = hotels.Select(t => new AddHotelToApplicationViewModel(t));
            return View(list);
            
        }

    }

    
}