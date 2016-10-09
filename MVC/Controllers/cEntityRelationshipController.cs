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
    public class cEntityRelationshipController : Controller
    {
        private EntitiesDBContext db = new EntitiesDBContext();


        private SelectList PopulateEntityDropDownList(object selectedEntity = null)
        {
            var entityQuery = from e in db.Entities
                                   orderby e.ID
                                   select e;
            return new SelectList(entityQuery, "ID", "ID", selectedEntity);
        }

        // GET: cEntityRelationship
        public ActionResult Index()
        {
            return View(db.Relationships.ToList());
        }

        // GET: cEntityRelationship/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cEntityRelationship cEntityRelationship = db.Relationships.Find(id);
            if (cEntityRelationship == null)
            {
                return HttpNotFound();
            }
            return View(cEntityRelationship);
        }

        // GET: cEntityRelationship/Create
        public ActionResult Create()
        {
            var viewModel = new EntityRelationshipViewModel();
            viewModel.entities = PopulateEntityDropDownList(null);
            return View(viewModel);
        }

        // POST: cEntityRelationship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,eRelType,nEntityA_ID,nEntityB_ID")] EntityRelationshipViewModel vm)
        {
            cEntityRelationship e = new cEntityRelationship();

            if (ModelState.IsValid)
            {
                e.entityA = db.Entities.Find(vm.nEntityA_ID);
                e.entityB = db.Entities.Find(vm.nEntityB_ID);
                e.relType = vm.eRelType;
                

                db.Relationships.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
            //return View();
        }

        // GET: cEntityRelationship/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cEntityRelationship cEntityRelationship = db.Relationships.Find(id);
            if (cEntityRelationship == null)
            {
                return HttpNotFound();
            }
            return View(cEntityRelationship);
        }

        // POST: cEntityRelationship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,relType")] cEntityRelationship cEntityRelationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cEntityRelationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cEntityRelationship);
        }

        // GET: cEntityRelationship/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cEntityRelationship cEntityRelationship = db.Relationships.Find(id);
            if (cEntityRelationship == null)
            {
                return HttpNotFound();
            }
            return View(cEntityRelationship);
        }

        // POST: cEntityRelationship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cEntityRelationship cEntityRelationship = db.Relationships.Find(id);
            db.Relationships.Remove(cEntityRelationship);
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
