using Project_TL.Models.DAL;
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
        private IOwnerRepository ownerRepo;
        private IContactPersonRepository contactRepo;
        private IBranchRepository branchRepo;
     
        private List<Hotel> hh;
        // GET: Search

        public SearchController(IHotelRepository hotelRepo, ISystemRepository sysRepo,IOwnerRepository ownerRepo,IContactPersonRepository contactRepo,IBranchRepository branchRepo) {
            this.hotelRepo = hotelRepo;
            this.sysRepo = sysRepo;
            this.ownerRepo = ownerRepo;
            this.contactRepo = contactRepo;
            this.branchRepo = branchRepo;
           

            hh = new List<Hotel>();           
            hh = hotelRepo.FindAll().ToList();
            hh.OrderBy(t => t.Branch);
            

        }
        public ActionResult Index()
        {



            IEnumerable<HotelViewModel> hvm = hh.Select(t => new HotelViewModel(t));

            return View(hvm);
        }

        public ActionResult Details(string hotelId)
        {
            //find the hotel in the repository
            // Hotel h = hotelRepo.FindByCode(hotelId);
            Hotel h = hh.FirstOrDefault(t => t.HotelId.Equals(hotelId));

            //check if the hotel exist (normally this can never give null unless DB failure)
            if (h == null)
            {
                return HttpNotFound();
            }

            //Use a viewmodel to display al the details of the hotel
            HotelViewModel hvm = new HotelViewModel(h);



            /* List<Syst> sy = new List<Syst>();
             Branch b = new Branch("Mercure");

             ContactPerson p = new ContactPerson("ik@gmail.com", "thomas",047514012);

             Owner o = new Owner("Jan");
             Adres a = new Adres("Rue Jean Rey", 75015, "Paris", 20, "France");
             Hotel h = new Hotel(a, b, 10, p, " 5", "thhtht", "5585", o,Status.FIL, sy);

             b.addHotel(h);
             p.addHotel(h);
             HotelViewModel hvm = new HotelViewModel(h);
             */
            return View(hvm);
        }


        public ActionResult Edit(string hotelId)
        {
            //find the hotel in the repository
            // Hotel h = hotelRepo.FindByCode(hotelId);
            Hotel h = hh.FirstOrDefault(t => t.HotelId.Equals(hotelId));

            //check if the hotel exist (normally this can never give null unless DB failure)
            if (h == null)
            {
                return HttpNotFound();
            }


            //Use a viewmodel to display al the details of the hotel
            
            EditHotelViewModel ehvm = new EditHotelViewModel(h, ownerRepo.FindAll().ToList(), contactRepo.FindAll().ToList(), branchRepo.FindAll().ToList(),sysRepo.FindAll().ToList());

            return View(ehvm);
        }
        [HttpPost]
        public ActionResult Edit(string hotelId,EditHotelViewModel ehvm)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Hotel hotel = hotelRepo.FindByCode(hotelId);
                    MapToHotel(ehvm, hotel);
                    hotelRepo.SaveChanges();
                    TempData["message"] = String.Format("Hotel {0} werd aangepast", hotel.Branch + hotel.Adres.City);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(ehvm);
        }

        public ActionResult Create()
        {
            Hotel hotel = new Hotel();
            EditHotelViewModel ehvm = new EditHotelViewModel(hotel,ownerRepo.FindAll().ToList(),contactRepo.FindAll().ToList(),branchRepo.FindAll().ToList(),sysRepo.FindAll().ToList());
            return View(ehvm);
        }
        [HttpPost]
        public ActionResult Create(EditHotelViewModel ehvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Hotel h = new Hotel();
                    hotelRepo.AddHotel(h);
                    MapToHotel(ehvm, h);
                    hotelRepo.SaveChanges();
                    TempData["message"] = String.Format("Hotel {0} werd gecreëerd", h.Branch + h.Adres.City);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Create", ehvm);
        }


        private void MapToHotel(EditHotelViewModel ehvm, Hotel hotel)
        {
            hotel.Branch = branchRepo.FindByName(ehvm.Branch.Where(x => x.Selected).FirstOrDefault().Text);
            hotel.ContactPerson = contactRepo.FindByName(ehvm.ContactPerson.Where(x => x.Selected).FirstOrDefault().Text);
            hotel.Email = ehvm.Email;
            hotel.Owner = ownerRepo.FindByName(ehvm.Owner.Where(x => x.Selected).FirstOrDefault().Text);
            hotel.TelephoneNumber = ehvm.TelephoneNumber;
            hotel.VatNumber = ehvm.VatNumber;
           

        }
    }
}