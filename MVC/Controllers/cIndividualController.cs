using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using MVC.DAL;
using MVC.Models;
using MVC.ViewModels;

using PagedList;

namespace MVC.Controllers
{
    [HandleError]
    public class cIndividualController : Controller
    {
        private EntitiesDBContext db = new EntitiesDBContext();

        IIndividualRepository _repository;

        public cIndividualController() : this(new EF_IndividualRepository()) { }
        public cIndividualController(IIndividualRepository repository)
        {
            _repository = repository;
        }

        // GET: cIndividual
        public ActionResult Index(int? id)
        {
            //var viewModel = new IndividualMatterData();
            //viewModel.EntityID = null;
            

            //viewModel.Individuals = db.Individuals
            //    .Include(c => c.CDD_Details)
            //    .OrderBy(e => e.ID).ToList();

            var  individuals = _repository.GetAllIndividuals()
                .OrderBy(e => e.Firstname).OrderBy(e => e.Surname)
                .ToList<cIndividual>();
                //.Include(c => c.CDD_Details)
                //.OrderBy(e => e.ID).ToList();

            //viewModel.Matters = null;

            

            if (id != null)
            {
                cIndividual indiv = _repository.GetIndividualByID((int)id);

                int nIndex = individuals.FindIndex(o=>o.ID == indiv.ID);
                individuals[nIndex] = indiv;

                //viewModel.EntityID = id.Value;
                //// Set the view model matters = the matters from the entity whose id is id
                //viewModel.Matters = viewModel.Individuals.Where(
                //    i => i.ID == id.Value).Single().Matters;

                //viewModel.EntityRelationships = viewModel.Individuals.Where(
                //    i => i.ID == id.Value).Single().IsA;

            }

            return View("Index", individuals.ToList());
            
            
        }

        // GET: cIndividual/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cIndividual cIndividual = db.Individuals.Find(id);
            if (cIndividual == null)
            {
                return HttpNotFound();
            }
            return View(cIndividual);
        }

        // GET: cIndividual/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name");
            return View();
        }

        // POST: cIndividual/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Surname,Firstname,Middlename,Previousname,DateOfBirth,PlaceOfBirth,Nationality,CountryOfResidence,Address1,Address2,Address3")] cIndividual cIndividual)
        {
            if (ModelState.IsValid)
            {
                //db.Entities.Add(cIndividual);
                //db.SaveChanges();
                _repository.CreateNewIndividual(cIndividual);
                
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name", cIndividual.ID);
            return View("Create", cIndividual);
        }

        // GET: cIndividual/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cIndividual cIndividual = db.Individuals.Find(id);
            if (cIndividual == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name", cIndividual.ID);
            return View(cIndividual);
        }

        // POST: cIndividual/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Surname,Firstname,Middlename,Previousname,DateOfBirth,PlaceOfBirth,Nationality,CountryOfResidence,Address1,Address2,Address3")] cIndividual cIndividual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cIndividual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name", cIndividual.ID);
            return View(cIndividual);
        }

        // GET: cIndividual/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cIndividual cIndividual = db.Individuals.Find(id);
            if (cIndividual == null)
            {
                return HttpNotFound();
            }
            return View(cIndividual);
        }

        // POST: cIndividual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cIndividual cIndividual = db.Individuals.Find(id);
            db.Entities.Remove(cIndividual);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Search(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var individuals = from i in db.Individuals
                           select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                individuals = individuals.Where(i => i.Surname.Contains(searchString)
                                       || i.Firstname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    individuals = individuals.OrderByDescending(i => i.Surname);
                    break;
                case "Date":
                    individuals = individuals.OrderBy(i =>i.DateOfBirth);
                    break;
                case "date_desc":
                    individuals = individuals.OrderByDescending(i => i.DateOfBirth);
                    break;
                default:
                    individuals = individuals.OrderBy(i => i.Surname);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(individuals.ToPagedList(pageNumber, pageSize));

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
