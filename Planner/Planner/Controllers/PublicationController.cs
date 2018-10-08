using Domain.Helpers;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Reports;
using Planner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Calculation;
using System.IO;
using SpreadsheetLight;
using Planner.Filters;

namespace Planner.Controllers
{
	[IncompleteProfileFilter]
	public class PublicationController : Controller
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
		// GET: Publication
		public ActionResult Index()
		{
			return View(PublicationReportBuilder.CreateForm11(user));
		}

		public ActionResult Create(string error)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var model = new CreatePublicationViewModel
				{
					NMDBs = db.NMBDs.ToList(),
					Error = error
				};

				return View(model);

			}

		}

        public ActionResult SendToDspace(string error)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var model = new CreatePublicationViewModel
                {
                    NMDBs = db.NMBDs.ToList(),
                    Error = error
                };

                return View(model);
            }
        }

        [HttpPost]
		public ActionResult Create(PublicationCreate model, HttpPostedFileBase file)
		{
			if (file == null)
			{
				return RedirectToAction("Create", new { error = "Файл не выбран" });
			}
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var filepath = ConfigurationManager.AppSettings["PublicationFolder"] + new Random().Next() + file.FileName.Substring(file.FileName.LastIndexOf('.'));
				try
				{

					var a = Server.MapPath(filepath);
					file.SaveAs(Server.MapPath(filepath));

                    var allAuthors = 1;
                    if (model.NewCollaboratorsNames != null)
                        allAuthors += model.NewCollaboratorsNames.Count;
                    if (model.CollaboratorsIds != null)
                        allAuthors += model.CollaboratorsIds.Count;

					Publication publication = new Publication()
					{
						Name = model.Name,
						FilePath = filepath,
						Output = model.Output,
						Pages = model.Pages,
						StoringType = new StoringType() { Value = model.StoringType },
						PublicationType = new PublicationType() { Value = model.PublicationType },
						CreatedAt = DateTime.Now.ToUniversalTime(),
						PublishedAt = model.PublishedAt.ToUniversalTime(),
						IsPublished = true,
						IsOverseas = model.IsOverseas,
						OwnerId = user.Id,
						CitationNumberNMBD = model.CitationNumberNMBD,
						ResearchDoneType = new ResearchDoneType() { Value = model.ResearchDoneType},
						ImpactFactorNMBD = model.ImpactFactorNMBD,
						PublicationNMBDs = new List<PublicationNMBD>()
						{
							new PublicationNMBD()
							{
								NMBDId = model.NMBDId
							}
						},
						PublicationUsers = new List<PublicationUser>
						{
							new PublicationUser()
							{
								UserId = user.Id,
								PageQuantity = model.Pages / allAuthors
							}
						}
					};
					if (model.CollaboratorsIds != null)
					{
						foreach (var collab in model.CollaboratorsIds.Where(x => x != String.Empty && x.Substring(0, 2) == "c_"))
						{
							publication.PublicationUsers.Add(new PublicationUser()
							{
								CollaboratorId = collab.Substring(2),
								Publication = publication,
								PageQuantity = model.Pages / allAuthors
							});
						}
						foreach (var collab in model.CollaboratorsIds.Where(x => x != String.Empty && x.Substring(0, 2) == "u_"))
						{
							publication.PublicationUsers.Add(new PublicationUser()
							{
								UserId = collab.Substring(2),
								Publication = publication,
								PageQuantity = model.Pages / allAuthors
							});
						}
					}
					if (model.NewCollaboratorsNames != null)
					{
						foreach (var collab in model.NewCollaboratorsNames.Where(x => x != String.Empty))
						{
							var newcollab = new ExternalCollaborator() { Name = collab };
							db.ExternalCollaborators.Add(newcollab);
							publication.PublicationUsers.Add(new PublicationUser()
							{
								Collaborator = newcollab,
								Publication = publication,
								PageQuantity = model.Pages / allAuthors
							});
						}
					}


					db.Publications.Add(publication);
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					System.IO.File.Delete(Server.MapPath(filepath));
					throw ex;
				}

				return RedirectToAction("Index");
			}
		}

        public ActionResult PrintForm11()
		{
            var model = PublicationReportBuilder.CreateForm11(user);
            if (model.Count > 0)
            {
                SLDocument doc = PublicationReportBuilder.PrintReportForm11(model, user);
                var ms = new MemoryStream();
                doc.SaveAs(ms);
                ms.Position = 0;
                var name = $"Публикации - {user.LastName} {user.FirstName.FirstOrDefault()}. {user.ThirdName.FirstOrDefault()}. .xlsx";
                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
            }
            return RedirectToAction("Index");

   //         var filestream = PublicationReportBuilder.PrintReportForm11(user);
			//return File(filestream, "application/vnd.ms-excel", $"Публикации {user.LastName} {user.FirstName.Substring(0, 1)}. {user.ThirdName.Substring(0, 1)}. - {DateTime.Now.ToShortDateString()}.xls".Replace('/','-'));
				
							
		}
	}
}