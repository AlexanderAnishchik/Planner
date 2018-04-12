using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Models;
using Newtonsoft.Json;
using System.IO;
using SpreadsheetLight;
using Calculation;
using Microsoft.AspNet.Identity;
using Planner.Filters;

namespace Planner.Controllers
{
    public static class IndivPlanTabNamesEnum
    {
        public static string ScientificPublishingTab { get { return "ScientificPublishing"; } }
    }

    [Authorize(Roles = "Teacher,Admin,TeacherModerator,HeadOfMethodologyDepartment")]
    [IncompleteProfileFilter]
    public class IndividualPlanController : Controller
    {


        // GET: IndividualPlan
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadReport()
        {
            string fileName = "Ind_plan.xlsm";
            SLDocument doc = PublicationReportBuilder.FacultyReportBuild(User.Identity.Name);
            var ms = new MemoryStream();
            doc.SaveAs(ms);
            ms.Position = 0;
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [Authorize(Roles = "Teacher,Admin,TeacherModerator")]
        public ActionResult TrainingJob()
        {
            using (var db = new ApplicationDbContext())
            {
                var indivPlanType = db.IndPlanTypes.Where(x => x.Name.Equals("TrainingJob")).AsEnumerable().Select(x => new IndPlanType() { Id = x.Id, Name = x.Name }).FirstOrDefault();
                return View(indivPlanType);
            }

        }
        [HttpPost]
        [Authorize(Roles = "Teacher,Admin,TeacherModerator,HeadOfMethodologyDepartment")]
        public ActionResult GetDataByType(string type)
        {
            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var fields = db.IndivPlanFields
                        .Join(db.IndPlanTypes, f => f.TypeId, t => t.Id, (f, t) => new { f, t })
                        .Where(x => x.t.Name.Equals(type));
            var grouped = fields.GroupBy(x => x.f.TabName, x => x, (key, res) => new
            {
                TabName = key,
                TabKey = Guid.NewGuid(),
                Fields = res.Select(x => new
                {
                    x.f.Id,
                    x.f.DisplayName,
                    x.f.SchemaName,
                    x.f.TabName,
                    Result = db.IndivPlanFieldsValues.Where(z => z.SchemaName == x.f.SchemaName && z.ApplicationUserId == userId).Select(vv => vv.Result).FirstOrDefault(),
                    PlannedValue = db.IndivPlanFieldsValues.Where(z => z.SchemaName == x.f.SchemaName && z.ApplicationUserId == userId).Select(vv => vv.PlannedValue).FirstOrDefault()
                })
            }).ToList();
            return Json(grouped);
        }
        [Authorize(Roles = "Teacher,Admin,TeacherModerator")]
        public ActionResult PlanScientificWork()
        {
            return View();
        }

        [Authorize(Roles = "Teacher,Admin,TeacherModerator,HeadOfMethodologyDepartment")]
        public ActionResult PlanMethodicalWork()
        {
            using (var db = new ApplicationDbContext())
            {
                var indivPlanType = db.IndPlanTypes.Where(x => x.Name.Equals("MethodicalJob")).AsEnumerable().Select(x => new IndPlanType() { Id = x.Id, Name = x.Name }).FirstOrDefault();
                return View(indivPlanType);
            }
        }

        [Authorize(Roles = "Teacher,Admin,TeacherModerator")]
        public ActionResult PlanManagment()
        {
            using (var db = new ApplicationDbContext())
            {
                var indivPlanType = db.IndPlanTypes.Where(x => x.Name.Equals("OrganizationalJob")).AsEnumerable().Select(x => new IndPlanType() { Id = x.Id, Name = x.Name }).FirstOrDefault();
                return View(indivPlanType);
            }
        }

        public string GetPlanTrainingJobs()
        {
            using (var db = new ApplicationDbContext())
            {
                var data = db.PlanTrainingJobs.ToList();
                return JsonConvert.SerializeObject(data);
            }
        }

        public void SavePlanTrainingJobs(PlanTrainingJob model)
        {
            using (var db = new ApplicationDbContext())
            {
                model.Id = Guid.NewGuid().ToString();
                db.Entry(model).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
        }

        public void DeletePlanTrainingJobs(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id != null)
                {
                    db.Entry(new PlanTrainingJob() { Id = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void EditPlanTrainingJobs(PlanTrainingJob model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (model.Id != null)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public string GetPlanScientificWork()
        {
            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();
                var data = db.IndivPlanFieldsValues.Where(x => x.ApplicationUserId == userId).Select(z => new
                {
                    z.Id,
                    z.PlannedValue,
                    z.Result,
                    z.SchemaName
                }).ToList();
                return JsonConvert.SerializeObject(data);
            }
        }

        public void SavePlanScientificWork(IndivPlanFieldsValue model)
        {
            using (var db = new ApplicationDbContext())
            {
                model.Id = Guid.NewGuid().ToString();
                db.Entry(model).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
        }

        public void DeletePlanScientificWork(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id != null)
                {
                    db.Entry(new IndivPlanFieldsValue() { Id = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
        public ActionResult SaveData(List<ScientificSaveDataHelper> model)
        {
            List<IndivPlanFieldsValue> userData;
            using (var db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();
                userData = db.IndivPlanFieldsValues.Where(x => x.ApplicationUserId == userId).ToList();

                model.ForEach(el =>
                {
                    if (userData.Select(x => x.SchemaName).Contains(el.SchemaName))
                    {
                        var update = userData.FirstOrDefault(x => x.SchemaName == el.SchemaName);
                        if (update != null)
                        {
                            update.Result = el.Value;
                            update.PlannedValue = el.PlannedValue;
                        }
                    }
                    else
                    {
                        if (el.Value != null || el.PlannedValue != null)
                            db.IndivPlanFieldsValues.Add(new IndivPlanFieldsValue { Result = el.Value, PlannedValue = el.PlannedValue, SchemaName = el.SchemaName, ApplicationUserId = userId });
                    }
                });
                db.SaveChanges();
            }
            return null;
        }
        public void EditPlanScientificWork(IndivPlanFieldsValue model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (model.Id != null)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public string GetPlanMethodicalWork()
        {
            using (var db = new ApplicationDbContext())
            {
                var data = db.PlanMethodicalWorks.ToList();
                return JsonConvert.SerializeObject(data);
            }
        }

        public void SavePlanMethodicalWork(PlanMethodicalWork model)
        {
            using (var db = new ApplicationDbContext())
            {
                model.Id = Guid.NewGuid().ToString();
                db.Entry(model).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
        }

        public void DeletePlanMethodicalWork(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id != null)
                {
                    db.Entry(new PlanMethodicalWork() { Id = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void EditPlanMethodicalWork(PlanMethodicalWork model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (model.Id != null)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public string GetPlanManagment()
        {
            using (var db = new ApplicationDbContext())
            {
                var data = db.PlanManagments.ToList();
                return JsonConvert.SerializeObject(data);
            }
        }

        public void SavePlanManagment(PlanManagment model)
        {
            using (var db = new ApplicationDbContext())
            {
                model.Id = Guid.NewGuid().ToString();
                db.Entry(model).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
        }

        public void DeletePlanManagment(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (id != null)
                {
                    db.Entry(new PlanManagment() { Id = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void EditPlanManagment(PlanManagment model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (model.Id != null)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }

    public class ScientificSaveDataHelper
    {
        public string SchemaName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string PlannedValue { get; set; }
    }
}