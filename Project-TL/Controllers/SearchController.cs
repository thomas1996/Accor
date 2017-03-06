using Project_TL.Models.Domain;
using Project_TL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class SearchController : Controller
    {
        private IHotelRepository hotelRepo;
        private ISystemRepository sysRepo;
        // GET: Search

            public SearchController(IHotelRepository hotelRepo,ISystemRepository sysRepo){
            this.hotelRepo = hotelRepo;
            this.sysRepo = sysRepo;
        }
        public ActionResult Index()
        {
            List<Syst> sy = new List<Syst>();
            Hotel h = new Hotel("Brussel", new Branch(), 10, new ContactPerson()," 5", "thhtht", "5585", new Owner(), sy);
            List<Hotel> hh = new List<Hotel>();
            hh.Add(h);
            return View(hh);
        }

        public  ActionResult Details(string hotelId)
        {
            Hotel h = hotelRepo.FindByCode(hotelId);
            if(h == null)
            {
                return HttpNotFound();
            }
            HotelViewModel hvm = new HotelViewModel(h);

            return View(hvm);
        }

    }
}