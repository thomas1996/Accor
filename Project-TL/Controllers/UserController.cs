using Project_TL.Models.DAL;
using Project_TL.Models.Domain;
using Project_TL.Models.Encryption;
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
        private DataProtection data;
        private string key = "34875BNYM==";

        public UserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
            data = new DataProtection();
        }     
        // GET: User
        public ActionResult Index()
        {
            User u = new User();
            ViewBag.user = u;
            //IEnumerable<UserViewModel> list = userRepo.findAll().Select(t => new UserViewModel(t));
            return View(userRepo.findAll());
        }

        public UserController()
        {

        }
        //public ActionResult Create()
        //{
        //    User u = new User();
        //    return PartialView("Index", new UserViewModel(u));
        //}

        [HttpPost]
        public ActionResult Create(UserViewModel uvm)
        {
            User u = new Models.Domain.User();
            MapToUser(u, uvm);
            userRepo.AddUser(u);
            userRepo.SaveChanges();
            TempData["message"] = $"User {u.Username} was created";
            return RedirectToAction("Index");

        }

        public ActionResult Delete(string username)
        {
            User u = userRepo.FindByUserName(username);
            if (u == null)
            {
                return HttpNotFound();
            }
            DeleteConfirmed(u);
            return RedirectToAction("Index");
        }
        
        public void DeleteConfirmed(User user)
        {
            try
            {              
                userRepo.RemoveUser(user);
                userRepo.SaveChanges();
                TempData["message"] = String.Format("User {0} was succesfully deleted",user.Username);
            }
            catch (Exception ex)
            {
                TempData["error"] = "There was a problem when trying to delete the user. If this problem keeps happeningn please contact IT department";
            }
            
        }

        private void MapToUser(User u, UserViewModel uvm)
        {
            u.Username = uvm.Username;
            u.Password = data.Encrypt("@ccor123",key);
            u.Location = uvm.Location;
            u.Admin = uvm.Admin;
        }
    }

}