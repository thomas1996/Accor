using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            List<Syst> sy = new List<Syst>();
            Hotel h = new Hotel("Brussel", new Branch(), 10, new ContactPerson(), 5, "thhtht", 5585, new Owner(), sy);
            List<Hotel> hh = new List<Hotel>();
            hh.Add(h);
            return View(hh);
        }
    }
}