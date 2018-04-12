using Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Planner.Filters;
using Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Planner.Controllers
{
	[IncompleteProfileFilter]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
                return View();
        }

        public ActionResult AboutTeachLoad()
        {
            return View();
        }


        public new ActionResult Profile()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!String.IsNullOrEmpty(User.Identity.Name))
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    ApplicationUser user = (from usr in db.Users where usr.Email == User.Identity.Name select usr).FirstOrDefault();
                    if (user != null)
                    {
                        EditModel model = new EditModel
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ThirdName = user.ThirdName,
                            DegreeEnum = user.Degree.Value,
                            PositionEnum = user.Position.Value,
                            AcademicTitleEnum = user.AcademicTitle.Value,
                            ScholarLink = user.ScholarLink,
                            OrcidLink = user.OrcidLink,
                        };

                        if (user.Degree != null)
                            model.DegreeEnum = user.Degree.Value;
                        if (user.Position != null)
                            model.PositionEnum = user.Position.Value;
                        if (user.AcademicTitle != null)
                            model.AcademicTitleEnum = user.AcademicTitle.Value;

                        return View(model);
                    }

                }


            return View();
        }
        public ActionResult Edit(EditModel model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = (from usr in db.Users where usr.Email == User.Identity.Name select usr).First();
                if (user != null)
                {

                    if (model.ProfilePicture != null)
                    {
                        byte[] image = new byte[model.ProfilePicture.ContentLength];
                        model.ProfilePicture.InputStream.Read(image, 0, Convert.ToInt32(model.ProfilePicture.ContentLength));
                        user.ProfilePicture = image;
                    }
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Profile", "Home");
        }
    }
}