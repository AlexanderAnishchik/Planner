using Calculation;
using Domain.Models;
using Planner.Filters;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planner.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
    {
        [Authorize(Roles = "Admin,HeadOfStudies")]
        public ActionResult DepartmentPublications()
        {
            return View();
        }

        [Authorize(Roles = "Admin,HeadOfStudies")]
        public ActionResult HalfYearDepartmentPublications()
		{
			return View();
		}

		public JsonResult Get()
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var a = db.Schedules.ToList();
				var departms = db.Departments
					.Select(x => new
					{
							Id = x.Id,
							Name = x.Name
					})
					.OrderBy(x => x.Name)
					.ToList();
				return new JsonResult() { Data = departms, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			}
		}

		public JsonResult DateRangeDepartmentReport(string depId, DateTime start, DateTime end)
		{
			var model = PublicationReportBuilder.CreateDeparmentReport(depId, start, end);
			return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}
		public ActionResult PrintDepartmentPublicationsReport(string depId, DateTime start, DateTime end)
		{
			var model = PublicationReportBuilder.CreateDeparmentReport(depId, start, end);
			if(model.Count > 0)
			{
                SLDocument doc = PublicationReportBuilder.PrintDepartmentReport(model);
                var ms = new MemoryStream();
                doc.SaveAs(ms);
                ms.Position = 0;
                var name = $"Публикации - {model[0].DepartmentName}";
                if (model[0].Start != null && model[0].End != null)
                {
                    name += $" за {model[0].Start.Value.ToShortDateString().Replace('/', '.')} - {model[0].End.Value.ToShortDateString().Replace('/', '.')}.xlsx";
                }
                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
			}
			return RedirectToAction("DepartmentPublications");
			
		}


        public JsonResult HalfYearDepartmentReport(string depId, int year, int half)
        {
            try
            {
                var model = PublicationReportBuilder.ScientificPublishing(depId, year, half);
                return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public ActionResult PrintHalfYearDepartmentReport(string depId, int year, int half)
        {
            try
            {
                var model = PublicationReportBuilder.ScientificPublishing(depId, year, half);
                SLDocument doc = PublicationReportBuilder.PrintHalfReport(model);
                var ms = new MemoryStream();
                doc.SaveAs(ms);
                ms.Position = 0;
                var name = "Звiт за пiврiччя-" + DateTime.UtcNow.ToLongDateString() + ".xlsx";
                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
            }
            catch (Exception)
            {
                return Redirect("/Department/HalfYearDepartmentPublications");
            }
        }
    }
}