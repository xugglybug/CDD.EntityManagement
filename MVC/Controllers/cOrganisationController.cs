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

namespace MVC.Controllers
{
    public class cOrganisationController : Controller
    {
        private EntitiesDBContext db = new EntitiesDBContext();

        // GET: cOrganisation
        public ActionResult Index(int? id)
        {
            var viewModel = new OrganisationMatterData();
            viewModel.EntityID = null;


            viewModel.Organisations = db.Organisations
                .Include(c => c.CDD_Details)
                .OrderBy(e => e.ID).ToList();

            viewModel.Matters = null;

            if (id != null)
            {
                viewModel.EntityID = id.Value;
                // Set the view model matters = the matters from the entity whose id is id
                viewModel.Matters = viewModel.Organisations.Where(
                    i => i.ID == id.Value).Single().Matters;
            }

            return View(viewModel);


        }

        // GET: cOrganisation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cOrganisation cOrganisation = db.Organisations.Find(id);
            if (cOrganisation == null)
            {
                return HttpNotFound();
            }
            return View(cOrganisation);
        }

        // GET: cOrganisation/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name");
            return View();
        }

        // POST: cOrganisation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,registeredName,tradingName,keyClient,organisationType,Address1,Address2,Address3")] cOrganisation cOrganisation)
        {
            if (ModelState.IsValid)
            {
                db.Entities.Add(cOrganisation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name", cOrganisation.ID);
            return View(cOrganisation);
        }

        // GET: cOrganisation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cOrganisation cOrganisation = db.Organisations.Find(id);
            if (cOrganisation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name", cOrganisation.ID);
            return View(cOrganisation);
        }

        // POST: cOrganisation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,registeredName,tradingName,keyClient,organisationType,Address1,Address2,Address3")] cOrganisation cOrganisation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOrganisation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.CDD_Details, "ID", "CDDContact_Name", cOrganisation.ID);
            return View(cOrganisation);
        }

        // GET: cOrganisation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cOrganisation cOrganisation = db.Organisations.Find(id);
            if (cOrganisation == null)
            {
                return HttpNotFound();
            }
            return View(cOrganisation);
        }

        // POST: cOrganisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cOrganisation cOrganisation = db.Organisations.Find(id);
            db.Entities.Remove(cOrganisation);
            db.SaveChanges();
            return RedirectToAction("Index");
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
