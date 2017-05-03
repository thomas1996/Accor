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
        public SettingsController(IBranchRepository branchRepo, IContactPersonRepository contactRepo, IOwnerRepository ownerRepo, IApplicationRepository systRepo,IHotelRepository hotelRepo)
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
            SettingsViewModel svm = new SettingsViewModel(branchRepo.FindAll().ToList(),contactRepo.FindAll().ToList(),ownerRepo.FindAll().ToList());
            return View(svm);
        }

        //branches
        public ActionResult DetailBranch(string name)
        {
            Branch b = branchRepo.FindByName(name);
            BranchViewModel bvm = new BranchViewModel(b,null);
            return View(bvm);
        }

        public ActionResult CreateBranch()
        {
            Branch b = new Branch();
            BranchViewModel bvm = new BranchViewModel(b,hotelRepo);
            return View(bvm);
        }

        [HttpPost]
        public ActionResult CreateBranch(BranchViewModel bvm)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Branch b = new Branch();
                    branchRepo.AddBranch(b);
                    MapToBranch(bvm,b);
                    branchRepo.SaveChanges();
                    TempData["message"] = String.Format("Branch {0} was created", b.Name);
                    return RedirectToAction("Index");
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View("CreateBranch", bvm);
        }

        private void MapToBranch(BranchViewModel bvm, Branch b)
        {
            throw new NotImplementedException();
        }
    }
}