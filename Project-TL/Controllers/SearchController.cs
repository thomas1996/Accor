﻿using Project_TL.Models.DAL;
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

        public SearchController(IHotelRepository hotelRepo, IApplicationRepository sysRepo,IOwnerRepository ownerRepo,IContactPersonRepository contactRepo,IBranchRepository branchRepo) {
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

        public ActionResult Delete(string id)
        {
            Hotel h = hotelRepo.FindByCode(id);
            if (h == null)
                return HttpNotFound();
            return View(h);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Hotel h = hotelRepo.FindByCode(id);               
                if(h == null)
                {
                    return HttpNotFound();
                }

                //removing the hotel from all the applications
                List<Application> apps = sysRepo.FindByHotel(h.HotelId).ToList();
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
            }catch(Exception ex)
            {
                TempData["error"] = String.Format("There has been a problem. If this keeps happening, please contact your administrator");
            }
            return RedirectToAction("Index");
        }

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
                    if (t.HotelId == id && DateTime.Compare(t.EndDate, DateTime.Today) < 0)
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