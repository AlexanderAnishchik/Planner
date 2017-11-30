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
    public class NMBDsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NMBDs
        public ActionResult Index()
        {
            return View(db.NMBDs.ToList());
        }

        // GET: NMBDs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NMBD nMBD = db.NMBDs.Find(id);
            if (nMBD == null)
            {
                return HttpNotFound();
            }
            return View(nMBD);
        }

        // GET: NMBDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NMBDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] NMBD nMBD)
        {
            if (ModelState.IsValid)
            {
                db.NMBDs.Add(nMBD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nMBD);
        }

        // GET: NMBDs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NMBD nMBD = db.NMBDs.Find(id);
            if (nMBD == null)
            {
                return HttpNotFound();
            }
            return View(nMBD);
        }

        // POST: NMBDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] NMBD nMBD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nMBD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nMBD);
        }

        // GET: NMBDs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NMBD nMBD = db.NMBDs.Find(id);
            if (nMBD == null)
            {
                return HttpNotFound();
            }
            return View(nMBD);
        }

        // POST: NMBDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NMBD nMBD = db.NMBDs.Find(id);
            db.NMBDs.Remove(nMBD);
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
