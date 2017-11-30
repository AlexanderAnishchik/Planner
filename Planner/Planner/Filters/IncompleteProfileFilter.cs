using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planner.Filters
{
	public class IncompleteProfileFilter : ActionFilterAttribute
	{
		public static readonly string IncompleteUserKeyName = "IncompleteUser";
		private string redirectPath = "~/Account/CompleteProfile";

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var session = filterContext.HttpContext.Session;
			if(session[IncompleteUserKeyName] != null && (string)session[IncompleteUserKeyName] == filterContext.RequestContext.HttpContext.User.Identity.Name)
			{
				filterContext.Result = new RedirectResult(redirectPath);
			}
			using (var db = new ApplicationDbContext())
			{
				ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == filterContext.RequestContext.HttpContext.User.Identity.Name);
				if(user == null)
				{
					filterContext.Result = new RedirectResult("Account/Login");
					return;
				}
				bool isAdmin = filterContext.RequestContext.HttpContext.User.IsInRole("Admin");
				if (!isAdmin && (user.AcademicTitleId == null || 
					user.DegreeId == null ||
					user.PositionId == null ||
					(user.DepartmentUsers == null ||
					user.DepartmentUsers?.Count == 0)))
				{
					session[IncompleteUserKeyName] = user.UserName;
					filterContext.Result = new RedirectResult(redirectPath);
				}
			}
			base.OnActionExecuting(filterContext);
		}
	}
}