using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Models;

namespace Planner.Controllers
{
    public class PlanChangesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanChanges
        public ActionResult Index()
        {
            var planChanges = db.PlanChanges.Include(p => p.ApplicationUser);
            return View(planChanges.ToList());
        }

        // GET: PlanChanges/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanChange planChange = db.PlanChanges.Find(id);
            if (planChange == null)
            {
                return HttpNotFound();
            }
            return View(planChange);
        }

        // GET: PlanChanges/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: PlanChanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Semester,TypesfJobs,Changes,PlannedVolume,ActualVolume,Base,Signature,ApplicationUserId")] PlanChange planChange)
        {
            if (ModelState.IsValid)
            {
                db.PlanChanges.Add(planChange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planChange.ApplicationUserId);
            return View(planChange);
        }

        // GET: PlanChanges/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanChange planChange = db.PlanChanges.Find(id);
            if (planChange == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planChange.ApplicationUserId);
            return View(planChange);
        }

        // POST: PlanChanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Semester,TypesfJobs,Changes,PlannedVolume,ActualVolume,Base,Signature,ApplicationUserId")] PlanChange planChange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planChange.ApplicationUserId);
            return View(planChange);
        }

        // GET: PlanChanges/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanChange planChange = db.PlanChanges.Find(id);
            if (planChange == null)
            {
                return HttpNotFound();
            }
            return View(planChange);
        }

        // POST: PlanChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlanChange planChange = db.PlanChanges.Find(id);
            db.PlanChanges.Remove(planChange);
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
