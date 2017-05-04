using Project_TL.Models.DAL;
using Project_TL.Models.Domain;
using Project_TL.ViewModels;
using Project_TL.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class SettingsController : Controller
    {
        private IBranchRepository branchRepo;
        private IContactPersonRepository contactRepo;
        private IOwnerRepository ownerRepo;
        private IApplicationRepository sysRepo;
        private IHotelRepository hotelRepo;
        private List<Status> status;
        private List<Models.Domain.Type> type;

        public SettingsController()
        {

        }
        public SettingsController(IBranchRepository branchRepo, IContactPersonRepository contactRepo, IOwnerRepository ownerRepo, IApplicationRepository systRepo, IHotelRepository hotelRepo)
        {
            this.branchRepo = branchRepo;
            this.hotelRepo = hotelRepo;
            this.contactRepo = contactRepo;
            this.ownerRepo = ownerRepo;
            this.sysRepo = systRepo;
            status = new List<Status>();
            type = new List<Models.Domain.Type>();
            MakeList("Status");
            MakeList("Type");
        }


        private void MakeList(string enums)
        {
            switch (enums.ToLower())
            {
                case "status":
                    {
                        foreach (Status s in Enum.GetValues(typeof(Status)))
                        {
                            status.Add(s);
                        }
                    }
                    break;
                case "type":
                    {
                        foreach (Models.Domain.Type t in Enum.GetValues(typeof(Models.Domain.Type)))
                        {
                            type.Add(t);
                        }
                    }
                    break;
            }
        }

        // GET: Settings
        public ActionResult Index()
        {
            SettingsViewModel svm = new SettingsViewModel(branchRepo.FindAll().ToList(), contactRepo.FindAll().ToList(), ownerRepo.FindAll().ToList());
            return View(svm);
        }

        //branches
        public ActionResult DetailBranch(string name)
        {
            Branch b = branchRepo.FindByName(name);
            if (b == null)
                return HttpNotFound();
            BranchViewModel bvm = new BranchViewModel(b);
            return View(bvm);
        }

        public ActionResult CreateBranch()
        {
            Branch b = new Branch();
            BranchViewModel bvm = new BranchViewModel(b);
            return View(bvm);
        }

        [HttpPost]
        public ActionResult CreateBranch(BranchViewModel bvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Branch b = new Branch();
                    branchRepo.AddBranch(b);
                    MapToBranch(bvm, b);
                    branchRepo.SaveChanges();
                    TempData["message"] = String.Format("Branch {0} was created", b.Name);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View("CreateBranch", bvm);
        }

        //contact person
        public ActionResult DetailsContactPerson(int id)
        {
            ContactPerson cp = contactRepo.FindById(id);
            if (cp == null)
                return HttpNotFound();
            ContactViewModel cvm = new ContactViewModel(cp);
            return View(cvm);
        }

        public ActionResult CreateContactPerson()
        {
            ContactPerson cp = new ContactPerson();
            ContactViewModel cvm = new ContactViewModel(cp);
            return View(cvm);

        }

        [HttpPost]
        public ActionResult CreateContactPerson(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            try
            {
                ContactPerson cp = new ContactPerson();
                contactRepo.AddContactPerson(cp);
                MapToContact(cp, model);
                contactRepo.SaveChanges();
                TempData["message"] = String.Format("Contact person {0} was succesfully created.", cp.LastName + cp.FirstName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "There was a problem creating the contact person. Please contact IT department.";
            }
            return View("CreateContactPerson", model);
        }

        //Owner
        public ActionResult DetailsOwner(int id)
        {
            Owner o = ownerRepo.FindById(id);
            if (o == null)
                return HttpNotFound();
            OwnerViewModel ovm = new OwnerViewModel(o);
            return View(ovm);
        }
        
        public ActionResult CreateOwner()
        {
            Owner o = new Owner();
            OwnerViewModel ovm = new OwnerViewModel(o);
            return View(ovm);
        }

        private void MapToContact(ContactPerson cp, ContactViewModel model)
        {
            cp.FirstName = model.FirstName;
            cp.LastName = model.LastName;
            cp.Email = model.Email;
            cp.CellphoneNr = model.CellphoneNr;
            cp.Hotels = model.Hotels;
        }

        private void MapToBranch(BranchViewModel bvm, Branch b)
        {
            b.Name = bvm.Name;
            b.PriceCat = bvm.PriceCat;

        }
    }
}