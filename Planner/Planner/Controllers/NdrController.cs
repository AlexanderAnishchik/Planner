using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Domain;
using Domain.Models;
using Planner.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Helpers;
using Planner.Filters;

namespace Planner.Controllers
{
    [Authorize]
	[IncompleteProfileFilter]
	public class NdrController : Controller
    {
        public NdrController()
        {

        }
        [Authorize(Roles = "Admin,HeadOfStudies,TeacherModerator")]
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Add()
        {
            return PartialView("Add");
        }
        public PartialViewResult Result()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
                var ndrs = db.NDR.Where(x => x.UserId == user.Id).ToList();
                return PartialView("Result", ndrs);
            }
           
        }
        public ActionResult Save(NDR model)
        {
            if (ModelState.IsValid)
            {

              
                Session["message"] = null;
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
                    model.UserId = user.Id;
                    db.NDR.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            Session["message"] = "Усi поля повиннi бути заповненi";
            return RedirectToAction("Index");
        }
    }
   
}