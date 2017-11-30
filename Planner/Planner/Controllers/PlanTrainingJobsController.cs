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
    public class PlanTrainingJobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanTrainingJobs
        public ActionResult Index()
        {
            var planTrainingJobs = db.PlanTrainingJobs.Include(p => p.ApplicationUser);
            return View(planTrainingJobs.ToList());
        }

        // GET: PlanTrainingJobs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanTrainingJob planTrainingJob = db.PlanTrainingJobs.Find(id);
            if (planTrainingJob == null)
            {
                return HttpNotFound();
            }
            return View(planTrainingJob);
        }

        // GET: PlanTrainingJobs/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: PlanTrainingJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EducationForm,OrderNumber,Subject,DSD,Course,CountStudent,GroupCode,PlannedLectures,DoneLectures,PlannedPract,DonePract,PlannedLaboratory,DoneLaboratory,PlannedSeminar,DoneSeminar,PlannedIndividual,DoneIndividual,PlannedConsultation,DoneConsultation,PlannedExamConsultation,DoneExamConsultation,PlannedCheckControl,DoneCheckControl,PlannedCheckLectureControl,DoneCheckLectureControl,PlannedEAT,DoneEAT,PlannedCGS,DoneCGS,PlannedCoursework,DoneCoursework,PlannedOffsetting,DoneOffsetting,PlannedSemestrExam,DoneSemestrExam,PlannedTrainingPract,DoneTrainingPract,PlannedStateExam,DoneStateExam,PlannedDiploma,DoneDiploma,PlannedPostgraduates,DonePostgraduates,PlannedDEK,DoneDEK,ApplicationUserId")] PlanTrainingJob planTrainingJob)
        {
            if (ModelState.IsValid)
            {
                db.PlanTrainingJobs.Add(planTrainingJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planTrainingJob.ApplicationUserId);
            return View(planTrainingJob);
        }

        // GET: PlanTrainingJobs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanTrainingJob planTrainingJob = db.PlanTrainingJobs.Find(id);
            if (planTrainingJob == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planTrainingJob.ApplicationUserId);
            return View(planTrainingJob);
        }

        // POST: PlanTrainingJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EducationForm,OrderNumber,Subject,DSD,Course,CountStudent,GroupCode,PlannedLectures,DoneLectures,PlannedPract,DonePract,PlannedLaboratory,DoneLaboratory,PlannedSeminar,DoneSeminar,PlannedIndividual,DoneIndividual,PlannedConsultation,DoneConsultation,PlannedExamConsultation,DoneExamConsultation,PlannedCheckControl,DoneCheckControl,PlannedCheckLectureControl,DoneCheckLectureControl,PlannedEAT,DoneEAT,PlannedCGS,DoneCGS,PlannedCoursework,DoneCoursework,PlannedOffsetting,DoneOffsetting,PlannedSemestrExam,DoneSemestrExam,PlannedTrainingPract,DoneTrainingPract,PlannedStateExam,DoneStateExam,PlannedDiploma,DoneDiploma,PlannedPostgraduates,DonePostgraduates,PlannedDEK,DoneDEK,ApplicationUserId")] PlanTrainingJob planTrainingJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planTrainingJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", planTrainingJob.ApplicationUserId);
            return View(planTrainingJob);
        }

        // GET: PlanTrainingJobs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanTrainingJob planTrainingJob = db.PlanTrainingJobs.Find(id);
            if (planTrainingJob == null)
            {
                return HttpNotFound();
            }
            return View(planTrainingJob);
        }

        // POST: PlanTrainingJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlanTrainingJob planTrainingJob = db.PlanTrainingJobs.Find(id);
            db.PlanTrainingJobs.Remove(planTrainingJob);
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
