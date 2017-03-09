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

        // GET: User
        public ActionResult Index(IUserRepository userRepo)
        {
            this.userRepo = userRepo;

            return View();
        }
    }
}