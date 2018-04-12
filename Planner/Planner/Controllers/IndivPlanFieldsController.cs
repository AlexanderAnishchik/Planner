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
    public class IndivPlanFieldsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IndivPlanFields
        public ActionResult Index()
        {
            var indivPlanFields = db.IndivPlanFields.Include(i => i.IndPlanType);
            return View(db.IndivPlanFields.OrderBy(item => item.DisplayName).ToList());
        }

        // GET: IndivPlanFields/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndivPlanFields indivPlanFields = db.IndivPlanFields.Find(id);
            if (indivPlanFields == null)
            {
                return HttpNotFound();
            }
            return View(indivPlanFields);
        }

        // GET: IndivPlanFields/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.IndPlanTypes, "Id", "Name");
            return View();
        }

        // POST: IndivPlanFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayName,SchemaName,Suffix,TabName,TypeId")] IndivPlanFields indivPlanFields)
        {
            if (ModelState.IsValid)
            {
                db.IndivPlanFields.Add(indivPlanFields);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.IndPlanTypes, "Id", "Name", indivPlanFields.TypeId);
            return View(indivPlanFields);
        }

        // GET: IndivPlanFields/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndivPlanFields indivPlanFields = db.IndivPlanFields.Find(id);
            if (indivPlanFields == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.IndPlanTypes, "Id", "Name", indivPlanFields.TypeId);
            return View(indivPlanFields);
        }

        // POST: IndivPlanFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisplayName,SchemaName,Suffix,TabName,TypeId")] IndivPlanFields indivPlanFields)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indivPlanFields).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.IndPlanTypes, "Id", "Name", indivPlanFields.TypeId);
            return View(indivPlanFields);
        }

        // GET: IndivPlanFields/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndivPlanFields indivPlanFields = db.IndivPlanFields.Find(id);
            if (indivPlanFields == null)
            {
                return HttpNotFound();
            }
            return View(indivPlanFields);
        }

        // POST: IndivPlanFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IndivPlanFields indivPlanFields = db.IndivPlanFields.Find(id);
            db.IndivPlanFields.Remove(indivPlanFields);
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
