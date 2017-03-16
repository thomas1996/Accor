using Project_TL.Models.DAL;
using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepo;
        private Context context;
        public UserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        // GET: User
        public ActionResult Index()
        {
            

            return View(userRepo.findAll());
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}