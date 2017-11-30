using Domain.Models;
using Planner.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Planner.Controllers
{
	[IncompleteProfileFilter]
	public class TimetableController : Controller
    {
        private ApplicationUser user;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.FirstOrDefault(x => x.UserName == requestContext.HttpContext.User.Identity.Name);
            }

        }
        // GET: Timetable
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var b = db.Users.Where(u => u.Id == user.Id).Select(x => x.Schedule.ApiId);
                return View();
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetAllDepartments()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var a = db.Schedules.ToList();
                var departms = db.Departments
                    .Select(x => new
                    {
                        Department = new
                        {
                            x.Id,
                            x.Name,
                            Schedule = db.Departments
                                  .Join(db.Schedules, d => d.Id, s => s.DepartmentId, (d, s) => new { d, s })
                                  .Where(z => z.d.Id == x.Id).OrderBy(d => d.d.Name)
                                  .Select(m => new { m.s.ApiId, m.s.UserName, m.s.Id }).OrderBy(m => m.UserName).ToList()
                        },
                    })
					.OrderBy(x => x.Department.Name)
                    .ToList();
                var currentSchedule = db.Users.Where(x => x.Id == user.Id).Select(x => x.Schedule.ApiId).FirstOrDefault();
                return new JsonResult() { Data = new { Departments=departms,Default=currentSchedule } };
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SetAsDefault(string Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var newDefSchedule = db.Schedules.Where(x => x.ApiId == Id).FirstOrDefault();
                var currentUser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                currentUser.Schedule = newDefSchedule;
                db.SaveChanges();
                return new JsonResult() { Data = "OK" };
            }

        }
    }
}