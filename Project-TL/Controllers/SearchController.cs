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
            Branch b = new Branch("Mercure");
           
            ContactPerson p = new ContactPerson("ik@gmail.com", "thomas");

            Owner o = new Owner("Jan");
            Adres a = new Adres("Rue Jean Rey", 75015, "Paris", 20, "France");
            Hotel h = new Hotel(a, b, 10, p," 5", "thhtht", "5585", o, sy);

            b.addHotel(h);
            p.addHotel(h);

            List<Hotel> hh = new List<Hotel>();
            hh.Add(h);

           IEnumerable<HotelViewModel> hvm =  hh.Select(t => new HotelViewModel(t));
            return View(hvm);
        }

        public  ActionResult Details(string hotelId)
        {
            /* Hotel h = hotelRepo.FindByCode(hotelId);
             if(h == null)
             {
                 return HttpNotFound();
             }
             HotelViewModel hvm = new HotelViewModel(h);

     */
            List<Syst> sy = new List<Syst>();
            Branch b = new Branch("Mercure");

            ContactPerson p = new ContactPerson("ik@gmail.com", "thomas");

            Owner o = new Owner("Jan");
            Adres a = new Adres("Rue Jean Rey", 75015, "Paris", 20, "France");
            Hotel h = new Hotel(a, b, 10, p, " 5", "thhtht", "5585", o, sy);

            b.addHotel(h);
            p.addHotel(h);
            HotelViewModel hvm = new HotelViewModel(h);

            return View(hvm);
        }

    }
}