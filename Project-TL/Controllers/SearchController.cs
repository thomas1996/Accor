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
        private Context context;
        private List<Hotel> hh;
        // GET: Search

        public SearchController(IHotelRepository hotelRepo, ISystemRepository sysRepo,IOwnerRepository ownerRepo,IContactPersonRepository contactRepo,IBranchRepository branchRepo) {
            this.hotelRepo = hotelRepo;
            this.sysRepo = sysRepo;
            this.ownerRepo = ownerRepo;
            this.contactRepo = contactRepo;
            this.branchRepo = branchRepo;
            context = new Context();

            Adres a1 = new Adres("Rue de l'industrie", "1000", "Brussels", 12, "Belgium");
            Adres a2 = new Adres("Schipholweg", "1171", "Badhoevedorp", 181, "The nederlands");
            Adres a3 = new Adres("Prof K J bavicklaan", "1183", "At", 1, "The nederlands");

            Branch b1 = new Branch("Adagio Acces");
            Branch b2 = new Branch("Ibis budget");

            ContactPerson p1 = new ContactPerson("H8602@Adagio-city.com", "Laurence", "Braem", 0032479670122);
            ContactPerson p2 = new ContactPerson("h0649-GM@Accor.com", " van der Graaf", "Martijn", 0031627081233);
            ContactPerson p3 = new ContactPerson("HA3P2-GM@accor.com", "Rutger", "Blom", 0031627081168);

            Owner o1 = new Owner("Georges", "Deom");
            Owner o2 = new Owner("", "");
            Owner o3 = new Owner("Rullems", "Dennis");

            Maintenance m = new Maintenance(new DateTime(), new DateTime(2018, 10, 10), 5000);

            Syst s1 = new Syst(10000.00, "Boekhouding", Models.Domain.Type.Rented, new DateTime(), new DateTime(2017, 03, 15), m);
            Syst s2 = new Syst(1500.00, "Reservaties", Models.Domain.Type.Rented, new DateTime(), new DateTime(2017, 03, 15), m);
            Syst s3 = new Syst(1500.00, "Personeelsprogramma", Models.Domain.Type.Rented, new DateTime(), new DateTime(2017, 03, 15), m);

            List<Syst> sys = new List<Syst>();
            sys.Add(s1);
            sys.Add(s2);

            List<Syst> sys2 = new List<Syst>();
            sys2.Add(s1);
            sys2.Add(s2);
            sys2.Add(s3);

            Hotel h1 = new Hotel(a1, b1, "BE 0817.220.446 ", p1, "8602", "H8602@Adagio-city.com", "+32 227 41 780", o1, sys);
            Hotel h2 = new Hotel(a2, b2, "BE 0635.611.207 ", p2, "0649", "h0649-GM@Accor.com", "+31 20 50 25 100", o3, sys);
            Hotel h3 = new Hotel(a3, b2, "BE 0635.611.207 ", p3, "0648", "HA3P2@accor.com", "+31 20 34 83 533", o2, sys2);

            p1.addHotel(h1);
            p2.addHotel(h2);
            p3.addHotel(h3);

            s1.addHotel(h1);
            s1.addHotel(h2);
            s1.addHotel(h1);
            s2.addHotel(h2);
            s3.addHotel(h3);

            b1.addHotel(h1);
            b2.addHotel(h2);
            b2.addHotel(h3);

            o1.addHotel(h1);
            o2.addHotel(h3);
            o3.addHotel(h2);

            //context.Hotels.Add(h1);
            //context.Hotels.Add(h2);
            //context.Hotels.Add(h3);

            //context.Branches.Add(b1);
            //context.Branches.Add(b2);
            //context.Owner.Add(o1);
            //context.Owner.Add(o2);
            //context.Owner.Add(o3);
            //context.Systems.AddRange(sys2);



            context.SaveChanges();
            context.Database.CreateIfNotExists();

            /* List<Syst> sy = new List<Syst>();
             Branch b = new Branch("Mercure");

             ContactPerson p = new ContactPerson("ik@gmail.com", "thomas");

             Owner o = new Owner("Jan");
             Adres a = new Adres("Rue Jean Rey", 75015, "Paris", 20, "France");
             Hotel h = new Hotel(a, b, 10, p," 5", "thhtht", "5585", o,Status.FIL, sy);

             b.addHotel(h);
             p.addHotel(h);
             */

            hh = new List<Hotel>();
            hh.Add(h1);
            hh.Add(h2);
            hh.Add(h3);

            hh.OrderBy(t => t.Branch);
            //hotelRepo.FindAll().ToList();

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
            EditHotelViewModel ehvm = new EditHotelViewModel(h, ownerRepo.FindAll().ToList(), contactRepo.FindAll().ToList(), branchRepo.FindAll().ToList());
            return View(ehvm;
        }
    }
}