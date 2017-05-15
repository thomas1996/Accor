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
        private IFirmRepository firmRepo;
        private IHotelRepository hotelRepo;
        private List<Status> status;
        private List<Models.Domain.ApplicationType> type;

        public SettingsController()
        {

        }
        public SettingsController(IBranchRepository branchRepo, IContactPersonRepository contactRepo, IOwnerRepository ownerRepo, IApplicationRepository systRepo, IHotelRepository hotelRepo,IFirmRepository firmRepo)
        {
            this.branchRepo = branchRepo;
            this.hotelRepo = hotelRepo;
            this.contactRepo = contactRepo;
            this.ownerRepo = ownerRepo;
            this.firmRepo = firmRepo;
            this.sysRepo = systRepo;
            status = new List<Status>();
            type = new List<Models.Domain.ApplicationType>();
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
                        foreach (Models.Domain.ApplicationType t in Enum.GetValues(typeof(Models.Domain.ApplicationType)))
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

        public ActionResult EditBranch(string name)
        {
            Branch b = branchRepo.FindByName(name);
            if (b == null)
                return HttpNotFound();
            BranchViewModel bvm = new BranchViewModel(b);
            return View(bvm);
        }
        
        [HttpPost]
        public ActionResult EditBranch(string name,BranchViewModel model)
        {
            try
            {
                Branch b = branchRepo.FindByName(name);
                MapToBranch(model, b);

                branchRepo.SaveChanges();
                TempData["message"] = "The branch was succesfully edited.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return EditBranch(name);
            }
        }
        public ActionResult DeleteBranch(string name)
        {
            Branch b = branchRepo.FindByName(name);
            if (b == null)
            {
                TempData["error"] = "The selected branch could not been found";
                return RedirectToAction("Index");
            }
            List<Branch> list = branchRepo.FindAll().Where(t => t.Name != name).OrderBy(t => t.Name).ToList();
            DeleteBranchViewModel dbvm = new DeleteBranchViewModel(name)
            {
                //select the default branch
                SelectedListItem = list.FirstOrDefault().BranchId.ToString(),
                //fill the list with all the branches
                List = list.Select(t => new SelectListItem
                {
                    Value = t.Name,
                    Text = t.Name
                })
            };
            return View(dbvm);
        }

        [HttpPost,ActionName("DeleteBranch")]
        public ActionResult DeleteBranchConfirmed(string name,DeleteBranchViewModel model)
        {
            try
            {
                Branch b = branchRepo.FindByName(name);
                if (b == null)
                    return HttpNotFound();

                Branch newBranch = branchRepo.FindByName(model.SelectedListItem);

                //loop through all the hotels of the old branch and replace it with the new branch
                b.Hotels.ToList().ForEach(t =>
                {
                    t.Branch = newBranch;
                });

                branchRepo.RemoveBranch(b);
                branchRepo.SaveChanges();
                TempData["message"] = String.Format("The branch {0} had succesfully been removed", b.Name);
                return RedirectToAction("Index");

            }catch(Exception ex)
            {
                TempData["error"] = "There has been a problem. Please contact the IT department";
            }
            return RedirectToAction("Index");
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

        public ActionResult DeleteContactPerson (int id)
        {
            ContactPerson cp = contactRepo.FindById(id);
            if(cp == null)
            {
                TempData["error"] = "There was a problem finding the contact person.";
                return RedirectToAction("Index");
            }
            List<ContactPerson> list = contactRepo.FindAll().Where(t => t.Email != cp.Email).OrderBy(t => t.LastName).ToList();
            DeleteContactPersonViewModel dcpvm = new DeleteContactPersonViewModel(cp)
            {
                SelectedContactId = list.FirstOrDefault().ContactPersonId,
                List = list.Select(t => new SelectListItem
                {
                    Value = t.ContactPersonId.ToString(),
                    Text = t.LastName +" " +  t.FirstName

                })
            };

            return View(dcpvm);
        }

        [HttpPost,ActionName("DeleteContactPerson")]
        public ActionResult DeleteContactPersonConfirmed(int id ,DeleteContactPersonViewModel model)
        {
            try
            {
                ContactPerson cp = contactRepo.FindById(id);
                if (cp == null)
                {
                    TempData["error"] = "There was e problem deleting the contact person. Please contact the IT department.";
                    RedirectToAction("Index");
                }

                //Changing the contactperson to a new one before deleting it.
                ContactPerson newContact = contactRepo.FindById(model.SelectedContactId);
                cp.Hotels.ToList().ForEach(t =>
                {
                    t.ContactPerson = newContact;
                });

                contactRepo.RemoveContactPerson(cp);
                contactRepo.SaveChanges();
                TempData["message"] = String.Format("The contact {0} is succesfully deleted", cp.LastName + cp.FirstName);
                RedirectToAction("Index");
            }catch(Exception ex)
            {
                TempData["error"] = "There was a problem deleting the contact person. Please contact the IT department.";
            }

            return RedirectToAction("Index");  
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

        [HttpPost]
        public ActionResult CreateOwner(OwnerViewModel model)
        {
            if (!ModelState.IsValid)
                return HttpNotFound();
            try
            {
                Owner o = new Owner();
                ownerRepo.AddOwner(o);
                MapToOwner(o, model);
                ownerRepo.SaveChanges();
                TempData["message"] = String.Format("The owner {0} has been created", o.LastName + o.FirstName);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                TempData["error"] = "There was a problem creating the owner. Please contact the IT department.";
            }
            return View("CreateOwner", model);
            
        }

        public ActionResult DeleteOwner(int id)
        {
            Owner o = ownerRepo.FindById(id);
            if(o == null)
            {
                TempData["error"] = "There was a problem deleting this owner. Please contact the IT department.";
                return RedirectToAction("Index");
            }
            List<Owner> listOwner = ownerRepo.FindAll().Where(t => t.OwnerId != id).OrderBy(t => t.LastName).ToList();
            List<Firm> listFirm = firmRepo.FindAll().Where(t => t.Owner.OwnerId != id).OrderBy(t => t.name).ToList();
            DeleteOwnerViewModel dovm = new DeleteOwnerViewModel(o)
            {
                SelectedOwner = listOwner.FirstOrDefault().OwnerId,
                ListOwner = listOwner.Select(t => new SelectListItem
                {
                    Value = t.OwnerId.ToString(),
                    Text = t.LastName + " " + t.FirstName
                })
            };

            return View(dovm);
        }

        [HttpPost,ActionName("DeleteOwner")]
        public ActionResult DeleteOwnerConfirmed(int id,DeleteOwnerViewModel model)
        {
            try
            {
                Owner o = ownerRepo.FindById(id);
                if (o == null)
                {
                    TempData["error"] = "There was a problem deleting the owner. Please contact the IT department.";
                    return RedirectToAction("Index");
                }
                Owner newOwner = ownerRepo.FindById(model.SelectedOwner);


                o.Hotels.ToList().ForEach(t =>
                {
                    t.Owner = newOwner;
                });
                o.Firms.ToList().ForEach(t =>
                {
                    t.Owner = newOwner;
                });

                ownerRepo.RemoveOwner(o);
                ownerRepo.SaveChanges();
                TempData["message"] = String.Format("The owner {0} has succesfully been removed", o.LastName + " " + o.FirstName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "There was a problem deleting the owner. Please contact the IT department.";
            }
            return RedirectToAction("Index");

        }

        private void MapToOwner(Owner o, OwnerViewModel model)
        {
            o.FirstName = model.FirstName;
            o.LastName = model.LastName;
            o.Email = model.Email;
            o.CellphoneNumber = model.CellphoneNumber;
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