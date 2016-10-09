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

using System.Data.Entity.Infrastructure;

namespace MVC.Controllers
{
    public class cEntityController : Controller
    {
        private EntitiesDBContext db = new EntitiesDBContext();

        // GET: cEntity
        public ActionResult Index(int? id)
        {
            var viewModel = new EntityMatterData();
            viewModel.EntityID = null;

            viewModel.Entities = db.Entities.OrderBy(e => e.ID).ToList();
            //viewModel.Individuals = db.Individuals.OrderBy(e => e.ID).ToList();
            //viewModel.Organisations = db.Organisations.OrderBy(e => e.ID).ToList();
            //.Include(i => i.OfficeAssignment)
            //.Include(i => i.Courses.Select(c => c.Department))
            //.OrderBy(i => i.LastName);
            viewModel.Matters = null;

            if (id != null)
            {
                viewModel.EntityID = id.Value;
                // Set the view model matters = the matters from the entity whose id is id
                viewModel.Matters = viewModel.Entities.Where(
                    i => i.ID == id.Value).Single().Matters;
            }

            return View(viewModel);
        }

        // GET: cEntity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cEntity cEntity = db.Entities.Find(id);
            if (cEntity == null)
            {
                return HttpNotFound();
            }
            return View(cEntity);
        }

        // GET: cEntity/Create
        public ActionResult Create()
        {
            
            return View();
        }



        // POST: cEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CDDContact_Name,CDDContact_TelNo,CDDContact_Email,ComplianceSignOff_Name,ComplianceSignOff_Date,CDDStatus")] cEntity cEntity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entities.Add(cEntity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            
            return View(cEntity);
        }

        // GET: cEntity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cEntity entity = db.Entities
                .Include(i => i.Matters)
                .Where(i => i.ID == id)
                .Single();

            PopulateAssignedMatterData(entity);

            if (entity == null)
            {
                return HttpNotFound();
            }

            return View(entity);
        }

        private void PopulateAssignedMatterData(cEntity entity)
        {
            var allMatters = db.Matters;
            var entityMatters = new HashSet<string>(entity.Matters.Select(m => m.ID));
            var viewModel = new List<AssignedMatterViewModel>();
            foreach (var matter in allMatters)
            {
                viewModel.Add(new AssignedMatterViewModel
                {
                    MatterNumber = matter.ID,
                    MatterDescription = matter.Description,
                    Assigned = entityMatters.Contains(matter.ID)
                });
            }
            ViewBag.Matters = viewModel;
        }

        // POST: cEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,CDDContact_Name,CDDContact_TelNo,CDDContact_Email,ComplianceSignOff_Name,ComplianceSignOff_Date,CDDStatus")] cEntity cEntity)
        public ActionResult Edit(int? id, string[] selectedMatters)
        {
            /*
            if (ModelState.IsValid)
            {
                db.Entry(cEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cEntity);
            */
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entityToUpdate = db.Entities
               .Include(e => e.Matters)
               .Where(e => e.ID == id)
               .Single();

            
            if (TryUpdateModel(entityToUpdate, "",
               new string[] { "CDDContact_Email", "CDDContact_Name", "CDDContact_TelNo",
                   "CDDStatus", "ComplianceSignOff_Date", "ComplianceSignOff_Name" }))
            {
                try
                {
                    //if (String.IsNullOrWhiteSpace(entityToUpdate.CDDStatus.OfficeAssignment.Location))
                    //{
                    //    instructorToUpdate.OfficeAssignment = null;
                    //}

                    UpdateEntityMattersCourses(selectedMatters, entityToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedMatterData(entityToUpdate);
            return View(entityToUpdate);

        }

        private void UpdateEntityMattersCourses(string[] selectedMatters, cEntity entityToUpdate)
        {
            if (selectedMatters == null)
            {
                entityToUpdate.Matters = new List<cMatter>();
                return;
            }

            var selectedMattersHS = new HashSet<string>(selectedMatters);
            var entityMatters = new HashSet<string>
                (entityToUpdate.Matters.Select(m => m.ID));
            foreach (var matter in db.Matters)
            {
                if (selectedMattersHS.Contains(matter.ID.ToString()))
                {
                    if (!entityMatters.Contains(matter.ID))
                    {
                        entityToUpdate.Matters.Add(matter);
                    }
                }
                else
                {
                    if (entityMatters.Contains(matter.ID))
                    {
                        entityToUpdate.Matters.Remove(matter);
                    }
                }
            }
        }

        // GET: cEntity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cEntity cEntity = db.Entities.Find(id);
            if (cEntity == null)
            {
                return HttpNotFound();
            }
            return View(cEntity);
        }

        // POST: cEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cEntity cEntity = db.Entities.Find(id);
            db.Entities.Remove(cEntity);
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
