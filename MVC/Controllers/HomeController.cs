using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC.DAL;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private EntitiesDBContext db = new EntitiesDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            EntityManagementStatsData x = new ViewModels.EntityManagementStatsData();

            x.nIndividuals = db.Individuals.Count();
            x.nOrganisations = db.Organisations.Count();
            x.nRelationships = db.Relationships.Count();

            return View(x);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}