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
            User u = new User();
            ViewBag.user = u;
            return View(userRepo.findAll());
        }

        public ActionResult Create()
        {
            User u = new User();
            return View("Index", new UserViewModel(u));
        }

        [HttpPost]
        public ActionResult Create(UserViewModel uvm)
        {
            User u = new Models.Domain.User();
            MapToUser(u, uvm);
            userRepo.AddUser(u);
            userRepo.SafeChanges();
            TempData["message"] = $"User {u.Username} was created";
            return View("Index");

        }

        public ActionResult Delete(string username)
        {
            User u = userRepo.FindByUserName(username);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string username)
        {
            try
            {
                User u = userRepo.FindByUserName(username);
                if (u == null)
                    return HttpNotFound();
                userRepo.RemoveUser(u);
                userRepo.SafeChanges();
                TempData["message"] = $"User {u.Username} was deleted";
            }
            catch (Exception ex)
            {
                TempData["error"] = "There was a problem when trying to delete the user. If this problem keeps happeningn please contact IT department";
            }
            return RedirectToAction("Index");
        }

        private void MapToUser(User u, UserViewModel uvm)
        {
            u.Username = uvm.Username;
            u.Password = "Accor@123";
            u.Location = uvm.Location;
            u.Admin = uvm.Admin;
        }
    }

}