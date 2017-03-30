using Project_TL.Models.DAL;
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
        private SystemRepository systRepo;
        public ApplicationController()
        {

        }
        public ApplicationController(SystemRepository systRepo)
        {
            this.systRepo = systRepo;
        }
        // GET: Application
        public ActionResult Index()
        {
            IEnumerable<ApplicationViewModel> list = systRepo.FindAll().ToList().Select(t => new ApplicationViewModel(t));
            
            return View(list);
        }
    }
}