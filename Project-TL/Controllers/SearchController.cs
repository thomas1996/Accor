using java.util;
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
        private IApplicationRepository sysRepo;
        private IOwnerRepository ownerRepo;
        private IContactPersonRepository contactRepo;
        private IBranchRepository branchRepo;

        private List<Hotel> hh;
        // GET: Search

        public SearchController(IHotelRepository hotelRepo, IApplicationRepository sysRepo, IOwnerRepository ownerRepo, IContactPersonRepository contactRepo, IBranchRepository branchRepo)
        {
            this.hotelRepo = hotelRepo;
            this.sysRepo = sysRepo;
            this.ownerRepo = ownerRepo;
            this.contactRepo = contactRepo;
            this.branchRepo = branchRepo;

            hh = hotelRepo.FindAll().ToList();
            hh.OrderBy(t => t.Name);

        }
        public ActionResult Index()
        {
            IEnumerable<HotelViewModel> hvm = hh.Select(t => new HotelViewModel(t));

            return View(hvm);
        }

        //CRUD methods Hotel

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

            EditHotelViewModel ehvm = new EditHotelViewModel(h);

            return View(ehvm);
        }
        [HttpPost]
        public ActionResult Edit(string hotelId, EditHotelViewModel ehvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Hotel hotel = hotelRepo.FindByCode(hotelId);
                    Context x = hotelRepo.getContext();
                    MapToHotel(ehvm, hotel,x);
                    hotelRepo.SaveChanges();
                    TempData["message"] = String.Format("Hotel {0} werd aangepast", hotel.Branch + hotel.Adres.City);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(ehvm);
        }

        public ActionResult Create()
        {
            //making the lists so that you only go to the Repo's once
            List<ContactPerson> contact = contactRepo.FindAll().OrderBy(t => t.LastName).ToList();
            List<Branch> branch = branchRepo.FindAll().OrderBy(t => t.Name).ToList();
            List<Owner> owner = ownerRepo.FindAll().OrderBy(t => t.LastName).ToList();
            List<Status> status = new List<Status>();

            foreach (Status s in Enum.GetValues(typeof(Status)))
            {
                status.Add(s);
            }



            Hotel hotel = new Hotel();
            EditHotelViewModel ehvm = new EditHotelViewModel(hotel)
            {
                //select the default branch
                SelectedBranchId = branch.FirstOrDefault().BranchId.ToString(),
                //fill the list with all the branches
                Branch = branch.Select(t => new SelectListItem
                {
                    Value = t.BranchId.ToString(),
                    Text = t.Name
                }),
                //repeat for al the other lists

                //Contactperson
                SelectedContactPersonId = contact.FirstOrDefault().ContactPersonId.ToString(),
                ContactPerson = contact.Select(t => new SelectListItem
                {
                    Value = t.ContactPersonId.ToString(),
                    Text = t.LastName + " " + t.FirstName
                }),

                //Owner
                SelectedOwnerId = owner.FirstOrDefault().OwnerId,
                Owner = owner.Select(t => new SelectListItem
                {
                    Value = t.OwnerId.ToString(),
                    Text = t.LastName + " " + t.FirstName
                }),

                //status
                SelectedStatusId = status.FirstOrDefault().ToString(),
                Status = status.Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString()
                })

            };

            return View(ehvm);
        }
        [HttpPost]
        public ActionResult Create(EditHotelViewModel ehvm)
        {
            //if (ModelState.IsValid)
            {
                try
                {
                    Hotel h = new Hotel();
                    hotelRepo.AddHotel(h);
                    Context x = hotelRepo.getContext();
                    MapToHotel(ehvm, h,x);
                    
                  
                    x.SaveChanges();
                    TempData["message"] = String.Format("Hotel {0} werd gecreëerd", h.Name);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            // http policy requires to fill the list again after the post
            ehvm.Branch = branchRepo.FindAll().Select(t => new SelectListItem
            {
                Value = t.Name,
                Text = t.Name
            });
            ehvm.ContactPerson = contactRepo.FindAll().Select(t => new SelectListItem
            {
                Value = t.ContactPersonId.ToString(),
                Text = t.LastName + " " + t.FirstName
            });
            ehvm.Owner = ownerRepo.FindAll().Select(t => new SelectListItem
            {
                Value = t.OwnerId.ToString(),
                Text = t.LastName + " " + t.FirstName
            });
            List<Status> status = new List<Status>();

            foreach (Status s in Enum.GetValues(typeof(Status)))
            {
                status.Add(s);
            }
            ehvm.Status = status.Select(t => new SelectListItem
            {
                Value = t.ToString(),
                Text = t.ToString()
            });

            return View("Create", ehvm);
        }

        public ActionResult Delete(string id)
        {
            Hotel h = hotelRepo.FindByCode(id);
            if (h == null)
                return HttpNotFound();
            return View(h);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Hotel h = hotelRepo.FindByCode(id);
                if (h == null)
                {
                    return HttpNotFound();
                }

                List<Application> apps = sysRepo.FindByHotel(h.HotelId).ToList();
                //removing the hotel from all the applications

                apps.ForEach(t =>
                {
                    t.Hotels.Where(s => s.ApplicationId == t.ApplicationId).Where(r => r.HotelId == id).ToList().ForEach(q =>
                    {
                        t.removeHotel(q);
                    });
                });


                hotelRepo.RemoveHotel(h);
                hotelRepo.SaveChanges();
                TempData["message"] = String.Format("Hotel {0} has been deleted", h.Branch);
            }
            catch (Exception ex)
            {
                TempData["error"] = String.Format("There has been a problem. If this keeps happening, please contact your administrator");
            }
            return RedirectToAction("Index");
        }


        // applications of the hotel crud methods

        public ActionResult AddApplication(string id)
        {
            Hotel h = hotelRepo.FindByCode(id);

            List<int> i = new List<int>();
            List<Application> systems = sysRepo.FindAll().ToList();

            //deleting the systems that the application already has
            //You can't delete a system while in the loop so first save the positions and after that loop it and delete the hotels
            foreach (Application s in systems)
            {
                s.Hotels.ToList().ForEach(t =>
                {
                    if (t.HotelId == id && DateTime.Compare(t.EndDate, DateTime.Today) > 0)
                        i.Add(systems.IndexOf(s));
                });
            }

            //after deleting a system the indexes change? Use 'j' to solve this

            for (int j = 0; j < i.Count(); j++)
            {
                systems.RemoveAt(i.ElementAt(j) - j);
            }

            IEnumerable<AddApplication> list = systems.Select(t => new AddApplication(t));
            AddApplicationToHotelViewModel model = new AddApplicationToHotelViewModel(list.ToList());
            return View(model);

        }

        [HttpPost, ActionName("AddApplication")]
        public ActionResult ConfirmAddApplication(string id, AddApplicationToHotelViewModel model)
        {
            Hotel h = hotelRepo.FindByCode(id);
            if (h == null)
                return HttpNotFound();
            try
            {
                for (int i = 0; i < model.Applications.Count(); i++)
                {
                    if (model.Applications[i].Checked == true)
                    {
                        Application app = sysRepo.FindById(model.Applications[i].ApplicationId);
                        HotelApplication ha = new HotelApplication();

                        //fill up the HA from the model etc
                        MapToHotelApp(ha, model.Applications[i], h, app);

                        app.addHotel(ha);
                        h.addApplication(ha);

                        hotelRepo.SaveChanges();
                        TempData["message"] = String.Format("Application {0} was succesfully added to the hotel", app.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "There was a problem when adding te application to the hotel. Please try again later";
                //if the app was added but the error came from de DB
                //h.removeApplication(sysRepo.FindById(ApplicationId).Hotels.Where(t => t.HotelId == id).First()); 
            }
            return RedirectToAction("Details", new { hotelId = id });
        }

        public ActionResult DeleteApplication(string id, int sysId, DateTime endDate)
        {
            try
            {
                Application app = sysRepo.FindById(sysId);
                Hotel h = hotelRepo.FindByCode(id);

                h.removeApplication(h.Applications.Where(t => t.ApplicationId == sysId).Where(s => s.HotelId == id).Where(r => r.EndDate.Equals(endDate)).FirstOrDefault());
                app.removeHotel(app.Hotels.Where(t => t.ApplicationId == sysId).Where(s => s.HotelId == id).Where(r => r.EndDate.Equals(endDate)).FirstOrDefault());
                hotelRepo.SaveChanges();
                TempData["message"] = "The system has succesfully been deleted from the hotel";
            }
            catch (Exception ex)
            {
                TempData["error"] = "There has been een error. Please try again or contact the IT department";
            }

            return RedirectToAction("Details", new { hotelId = id });
        }

        private void MapToHotelApp(HotelApplication ha, AddApplication model, Hotel h, Application app)
        {
            ha.ApplicationId = app.ApplicationId;
            ha.ApplicationName = app.Name;
            ha.Cost = model.Cost;
            ha.EndDate = model.EndDate;
            ha.StartDate = model.StartDate;
            ha.HotelId = h.HotelId;
            ha.HotelName = h.Name;

        }

        private void MapToHotel(EditHotelViewModel ehvm, Hotel hotel,Context x)
        {
            //hotel.Branch = branchRepo.FindById(Convert.ToInt32(ehvm.SelectedBranchId));
            //hotel.ContactPerson = contactRepo.FindById(Convert.ToInt32(ehvm.SelectedContactPersonId));
            //hotel.Email = ehvm.Email;
            //hotel.Owner = ownerRepo.FindById(Convert.ToInt32(ehvm.SelectedOwnerId));
            //hotel.TelephoneNumber = ehvm.TelephoneNumber;
            //hotel.VatNumber = ehvm.VatNumber;
            //hotel.Adres = ehvm.Adres;
            //hotel.HotelId = ehvm.HotelId;
            //hotel.Name = ehvm.Name;
            //hotel.Status = getEnum(ehvm.SelectedStatusId);

            int br = (Convert.ToInt32(ehvm.SelectedBranchId));
            int cp = (Convert.ToInt32(ehvm.SelectedContactPersonId));
            int ow = (Convert.ToInt32(ehvm.SelectedOwnerId));

            hotel.Branch = x.Branches.FirstOrDefault(t => t.BranchId == br );
            hotel.ContactPerson = x.ContactPersons.FirstOrDefault(t => t.ContactPersonId ==cp );
            hotel.Email = ehvm.Email;
            hotel.Owner = x.Owner.FirstOrDefault(t => t.OwnerId == ow );
            hotel.TelephoneNumber = ehvm.TelephoneNumber;
            hotel.VatNumber = ehvm.VatNumber;
            hotel.Adres = ehvm.Adres;
            hotel.HotelId = ehvm.HotelId;
            hotel.Name = ehvm.Name;
            hotel.Status = getEnum(ehvm.SelectedStatusId);


        }
        private Status getEnum(string s)
        {
            switch (s.ToUpper())
            {
                case "HQI": return Status.HQI;
                case "HQS": return Status.HQS;
                case "JV": return Status.JV;
                case "FR": return Status.FR;
                case "FIL": return Status.FIL;
                case "MAN": return Status.MAN;
                default:return Status.FR;

            }
        }
    }
}