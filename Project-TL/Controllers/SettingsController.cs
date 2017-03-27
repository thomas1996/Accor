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
    public class SettingsController : Controller
    {
        private BranchRepository branchRepo;
        private ContactPersonRepository contactRepo;
        private OwnerRepository ownerRepo;
        private SystemRepository sysRepo;
        private List<Status> status;
        private List<Models.Domain.Type> type;

        public SettingsController(BranchRepository branchRepo, ContactPersonRepository contactRepo, OwnerRepository ownerRepo, SystemRepository systRepo)
        {
            this.branchRepo = branchRepo;
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

        public ActionResult DetailBranch(int id)
        {

        }



    }
}