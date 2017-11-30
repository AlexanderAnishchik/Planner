using Domain.Helpers;
using Domain.Models;
using Planner.Filters;
using Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Planner.Controllers
{
	[Authorize]
	public class AuthorController : Controller
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
		// GET: Author
		public JsonResult Get()
        {
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var users = db.Users
					.Where(u => u.Id != user.Id)
						.AsEnumerable()
						.Select(u => new Author()
						{
							UserId = "u_" + u.Id,
							CollaboratorId = null,
							Name = $"{u.LastName} {u.FirstName} {u.ThirdName}"
						})
						.OrderBy(x => x.Name).ToList();
				var collaborators = db.ExternalCollaborators
						.AsEnumerable()
						.Select(u => new Author()
						{
							UserId = null,
							CollaboratorId = "c_" + u.Id,
							Name = u.Name
						})
						.OrderBy(x => x.Name).ToList();
				var model = new List<Author>();
				model.AddRange(users);
				model.AddRange(collaborators);
				return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

			}
		}
    }
}