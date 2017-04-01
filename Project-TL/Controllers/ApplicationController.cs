using Project_TL.Models.DAL;
using Project_TL.Models.Domain;
using Project_TL.ViewModels.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class ApplicationController : Controller
    {
        private ISystemRepository systRepo;
        public ApplicationController()
        {

        }
        public ApplicationController(ISystemRepository systRepo)
        {
            this.systRepo = systRepo;
        }
        // GET: Application
        public ActionResult Index()
        {
            IEnumerable<ApplicationViewModel> list = systRepo.FindAll().ToList().Select(t => new ApplicationViewModel(t));
            
            return View(list);
        }

        public ActionResult Details(int id)
        {
           Syst s = systRepo.FindById(id);
           ApplicationViewModel avw = new ApplicationViewModel(s);
            return View(avw);
        }
    }
}