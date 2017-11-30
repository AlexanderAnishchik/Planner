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
    public class PlanConclusionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanConclusions
        public ActionResult Index()
        {
            var planConclusions = db.PlanConclusions.Include(p => p.ApplicationUser);
            return View(planConclusions.ToList());
        }

        // GET: PlanConclusions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanConclusion planConclusion = db.PlanConclusions.Find(id);
            if (planConclusion == null)
            {
                return HttpNotFound();
            }
            return View(planConclusion);
        }

        // GET: PlanConclusions/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: PlanConclusions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Semester,Content,Signature,ApplicationUserId")] PlanConclusion planConclusion)
        {
            if (ModelState.IsValid)
            {
                db.PlanConclusions.Add(planConclusion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planConclusion.ApplicationUserId);
            return View(planConclusion);
        }

        // GET: PlanConclusions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanConclusion planConclusion = db.PlanConclusions.Find(id);
            if (planConclusion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planConclusion.ApplicationUserId);
            return View(planConclusion);
        }

        // POST: PlanConclusions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Semester,Content,Signature,ApplicationUserId")] PlanConclusion planConclusion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planConclusion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planConclusion.ApplicationUserId);
            return View(planConclusion);
        }

        // GET: PlanConclusions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanConclusion planConclusion = db.PlanConclusions.Find(id);
            if (planConclusion == null)
            {
                return HttpNotFound();
            }
            return View(planConclusion);
        }

        // POST: PlanConclusions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlanConclusion planConclusion = db.PlanConclusions.Find(id);
            db.PlanConclusions.Remove(planConclusion);
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
