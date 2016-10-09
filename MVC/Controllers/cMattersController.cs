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

namespace MVC.Controllers
{
    public class cMattersController : Controller
    {
        private EntitiesDBContext db = new EntitiesDBContext();

        // GET: cMatters
        public ActionResult Index()
        {
            var matters = db.Matters.Include(c => c.Office);
            return View(matters.ToList());
        }

        // GET: cMatters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cMatter cMatter = db.Matters.Find(id);
            if (cMatter == null)
            {
                return HttpNotFound();
            }
            return View(cMatter);
        }

        // GET: cMatters/Create
        public ActionResult Create()
        {
            ViewBag.OfficeID = new SelectList(db.Offices, "ID", "OfficeCode");
            return View();
        }

        // POST: cMatters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,OfficeID,Team,ResponsiblePartner,MatterManager,Sec_PA,Advice_Provided_Without_CDD")] cMatter cMatter)
        {
            if (ModelState.IsValid)
            {
                db.Matters.Add(cMatter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OfficeID = new SelectList(db.Offices, "ID", "OfficeCode", cMatter.OfficeID);
            return View(cMatter);
        }

        // GET: cMatters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cMatter cMatter = db.Matters.Find(id);
            if (cMatter == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfficeID = new SelectList(db.Offices, "ID", "OfficeCode", cMatter.OfficeID);
            return View(cMatter);
        }

        // POST: cMatters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,OfficeID,Team,ResponsiblePartner,MatterManager,Sec_PA,Advice_Provided_Without_CDD")] cMatter cMatter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cMatter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfficeID = new SelectList(db.Offices, "ID", "OfficeCode", cMatter.OfficeID);
            return View(cMatter);
        }

        // GET: cMatters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cMatter cMatter = db.Matters.Find(id);
            if (cMatter == null)
            {
                return HttpNotFound();
            }
            return View(cMatter);
        }

        // POST: cMatters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            cMatter cMatter = db.Matters.Find(id);
            db.Matters.Remove(cMatter);
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
