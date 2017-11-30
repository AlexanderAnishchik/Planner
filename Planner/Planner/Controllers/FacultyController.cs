using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Models;
using Planner.Filters;

namespace Planner.Controllers
{
	public class FacultyController : Controller
    {
        // GET: Faculty
        public JsonResult Get()
        {
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				return new JsonResult()
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = db.Faculties.Include("Departments").Select(x => new
					{
						Id = x.Id,
						Name = x.Name,
						Departments = x.Departments.Select(d => new { Id = d.Id, Name = d.Name }).OrderBy(d => d.Name).ToList()
					})
					.OrderBy(x => x.Name).ToList()
				};
			}
				
        }
    }
}