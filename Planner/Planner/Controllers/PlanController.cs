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
	[Authorize]
	[IncompleteProfileFilter]
	public class PlanController : Controller
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
		// GET: Plan
		public ActionResult Index()
        {
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				return View(db.ScientificPublishings.Where(x => x.UserId == user.Id).ToList());
			}
        }

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(ScientificPublishing model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    model.UserId = user.Id;
                    db.ScientificPublishings.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
		}
    }
}