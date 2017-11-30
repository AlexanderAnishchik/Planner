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
    public class PlanRemarksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanRemarks
        public ActionResult Index()
        {
            var planRemarks = db.PlanRemarks.Include(p => p.ApplicationUser);
            return View(planRemarks.ToList());
        }

        // GET: PlanRemarks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanRemark planRemark = db.PlanRemarks.Find(id);
            if (planRemark == null)
            {
                return HttpNotFound();
            }
            return View(planRemark);
        }

        // GET: PlanRemarks/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: PlanRemarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Remark,Signature,ApplicationUserId")] PlanRemark planRemark)
        {
            if (ModelState.IsValid)
            {
                db.PlanRemarks.Add(planRemark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planRemark.ApplicationUserId);
            return View(planRemark);
        }

        // GET: PlanRemarks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanRemark planRemark = db.PlanRemarks.Find(id);
            if (planRemark == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planRemark.ApplicationUserId);
            return View(planRemark);
        }

        // POST: PlanRemarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Remark,Signature,ApplicationUserId")] PlanRemark planRemark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planRemark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planRemark.ApplicationUserId);
            return View(planRemark);
        }

        // GET: PlanRemarks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanRemark planRemark = db.PlanRemarks.Find(id);
            if (planRemark == null)
            {
                return HttpNotFound();
            }
            return View(planRemark);
        }

        // POST: PlanRemarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlanRemark planRemark = db.PlanRemarks.Find(id);
            db.PlanRemarks.Remove(planRemark);
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
