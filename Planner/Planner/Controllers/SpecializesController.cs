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
    public class SpecializesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specializes
        public ActionResult Index()
        {
            return View(db.Specializes.OrderBy(item => item.Cipher).ToList());
        }

        // GET: Specializes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialize specialize = db.Specializes.Find(id);
            if (specialize == null)
            {
                return HttpNotFound();
            }
            return View(specialize);
        }

        // GET: Specializes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specializes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cipher")] Specialize specialize)
        {
            if (ModelState.IsValid)
            {
                db.Specializes.Add(specialize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialize);
        }

        // GET: Specializes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialize specialize = db.Specializes.Find(id);
            if (specialize == null)
            {
                return HttpNotFound();
            }
            return View(specialize);
        }

        // POST: Specializes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cipher")] Specialize specialize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialize);
        }

        // GET: Specializes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialize specialize = db.Specializes.Find(id);
            if (specialize == null)
            {
                return HttpNotFound();
            }
            return View(specialize);
        }

        // POST: Specializes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Specialize specialize = db.Specializes.Find(id);
            db.Specializes.Remove(specialize);
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
