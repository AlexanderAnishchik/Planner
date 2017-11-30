using System;
using System.Linq;
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
using Planner.Filters;

namespace Planner.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
		[Authorize(Roles = "User")]
		public JsonResult GetUserInfo()
		{
			using (var db = new ApplicationDbContext())
			{
				var user = db.Users.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
				return new JsonResult()
				{
					Data = new
					{
						
						AcademicTitle = ((DisplayAttribute)typeof(AcademicTitleEnum)
								.GetMember(user.AcademicTitle.Value.ToString())[0]
								.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,
						Degree = ((DisplayAttribute)typeof(DegreeEnum)
								.GetMember(user.Degree.Value.ToString())[0]
								.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,
						Email = user.Email,
						FirstName = user.FirstName,
						Id = user.Id,
						LastName = user.LastName,
						Position = ((DisplayAttribute)typeof(PositionEnum)
								.GetMember(user.Position.Value.ToString())[0]
								.GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,
						ScholarLink = user.ScholarLink,
						OrcidLink = user.OrcidLink,
						ThirdName = user.ThirdName,
						UserName = user.UserName
					},
					JsonRequestBehavior = JsonRequestBehavior.AllowGet
				};
			}
		}

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
			if (HttpContext.User.Identity.IsAuthenticated)
				return RedirectToAction("Profile", "Home");
            ViewBag.ReturnUrl = returnUrl;

            ////HACK
            //using (ApplicationDbContext context = new ApplicationDbContext())
            //{
            //    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //    var admin = new ApplicationUser { Email = "administrator@gmail.com", UserName = "administrator@gmail.com", FirstName = "Admin", LastName = "Admin", ThirdName = "Admin" };
            //    string password = "Administrator123123!!!";
            //    var result = userManager.Create(admin, password);

            //    // Success
            //    if (result.Succeeded)
            //    {
            //        // Add role for user
            //        userManager.AddToRole(admin.Id, "Admin");
            //        //userManager.AddToRole(admin.Id, role2.Name);
            //    }
            //}
                


            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = UserManager.FindByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Користувач не зареєстрований.");
                return View(model);
            }
			if (!user.IsActive)
			{
				ModelState.AddModelError("", "Користувач був деактивований.");
				return View(model);
			}
			// This doesn't count login failures towards account lockout
			// To enable password failures to trigger account lockout, change to shouldLockout: true
			var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Profile", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                default:
                    ModelState.AddModelError("", "Невірний код.");
                    return View(model);
            }
        }

		[Authorize(Roles = "Admin")]
		public ActionResult Register(string username)
		{
		    if (username != null)
			{
				ViewBag.userAdd = "Користувач " + username + ", був створенний!";
			}
		    using (new ApplicationDbContext())
		    {
		        return View();
		    }
		}

        //
		// POST: /Account/Register
		[HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult> Register(RegisterViewModel model)
        {
		    if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ThirdName = model.ThirdName,
                    Degree = new Degree() {Value = model.DegreeEnum},
                    Position = new Position() {Value = model.PositionEnum},
                    AcademicTitle = new AcademicTitle() {Value = model.AcademicTitleEnum},
                    ScholarLink = model.ScholarLink,
                    OrcidLink = model.OrcidLink,
                    IsActive=true,
                    DepartmentUsers = new List<DepartmentUser>
                    {
                        new DepartmentUser()
                        {
                            DepartmentId = model.DepartmentId
                        }
                    }
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
					UserManager.AddToRole(user.Id, model.Role);
                    return RedirectToAction("Register", new {username=user.Email });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View("Register", model);
        }

        [Authorize]
        public JsonResult GetRoles()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var roles = db.Roles.Select(x => new { x.Name }).ToList();
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = roles
                };
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public JsonResult GetUsersData()
        {
            GetUsersModel model = new GetUsersModel
            {
                UserList = UserManager.Users.Select(u => new EditingUser
                {
                    Id = u.Id,
                    Name = u.LastName + " " + u.FirstName + " " + u.ThirdName,
                    Email = u.Email,
                    PositionId = u.PositionId,
                    IsActive = u.IsActive
                })
                .OrderBy(x => x.Name)
                .ToList()
            };
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = model
            };
        }

        [Authorize(Roles = "Admin")]
        public JsonResult ToggleActive(string userName)
        {
            var user = UserManager.FindByEmail(userName);
            if (user == null)
                return Json(new { success = false, message = "Користувач не iснує." });

            user.IsActive = !user.IsActive;

            IdentityResult result = UserManager.Update(user);
            return result.Succeeded ? Json(new { success = true }) : Json(new { success = false, message = "Помилка пiд час запиту." });
        }

        public ActionResult Edit(string userName)
        {
			if(userName == null)
			{
				return Redirect("~/");
			}
            ApplicationUser user = UserManager.FindByEmail(userName);
			
            if (user != null)
            {
                EditModel model = new EditModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ThirdName = user.ThirdName,
                    ScholarLink = user.ScholarLink,
                    OrcidLink = user.OrcidLink,
					Role = UserManager.GetRoles(user.Id).FirstOrDefault()

                };

                if (user.Degree != null)
                    model.DegreeEnum = user.Degree.Value;
                if (user.Position != null)
                    model.PositionEnum = user.Position.Value;
                if (user.AcademicTitle != null)
                    model.AcademicTitleEnum = user.AcademicTitle.Value;

				//logic for faculty and departments
				if (user.DepartmentUsers != null && user.DepartmentUsers.Count != 0)
				{
					model.FacultyId = user.DepartmentUsers.FirstOrDefault().Department.FacultyId;
					model.DepartmentId = user.DepartmentUsers.FirstOrDefault().DepartmentId;
				}

                return View(model);
            }
            return RedirectToAction("Profile", "Home");
        }

        public FileContentResult GetProfilePic(string userName)
        {
            ApplicationUser user = UserManager.FindByEmail(userName);
            if (user != null)
            {
                if (user.ProfilePicture != null)
                    return new FileContentResult(user.ProfilePicture, "image/jpeg");
                else return null;
            }
            else return null;
        }
		public ActionResult CompleteProfile()
		{
			var user = UserManager.FindByName(User.Identity.GetUserName());
			if(user == null)
			{
				ModelState.AddModelError("", "Користувач не зареєстрований.");
				return View("Login");
			}
			EditModel model = new EditModel
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				ThirdName = user.ThirdName,
				ScholarLink = user.ScholarLink,
				OrcidLink = user.OrcidLink,
				Role = UserManager.GetRoles(user.Id).FirstOrDefault()

			};

			if (user.Degree != null)
				model.DegreeEnum = user.Degree.Value;
			if (user.Position != null)
				model.PositionEnum = user.Position.Value;
			if (user.AcademicTitle != null)
				model.AcademicTitleEnum = user.AcademicTitle.Value;

			//logic for faculty and departments
			if (user.DepartmentUsers != null && user.DepartmentUsers.Count != 0)
			{
				model.FacultyId = user.DepartmentUsers.FirstOrDefault().Department.FacultyId;
				model.DepartmentId = user.DepartmentUsers.FirstOrDefault().DepartmentId;
			}

			return View(model);
		}
		[HttpPost]
		public ActionResult CompleteProfile(EditModel user)
		{
			// double check for EditModel
			if (user.DepartmentId == null ||
					user.FacultyId == null || 
					user.AcademicTitleEnum == 0 || 
					user.DegreeEnum == 0 || 
					user.PositionEnum == 0)
			{
				return RedirectToAction("CompleteProfile");
			}
			Session.Remove(IncompleteProfileFilter.IncompleteUserKeyName);
			return Edit(user);
		}

		// POST: /Account/Edit
		[HttpPost]
        public ActionResult Edit(EditModel model)
        {
            ApplicationUser user = UserManager.FindByEmail(model.Email);
            if (user != null)
            {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.ThirdName = model.ThirdName;
                user.Degree = new Degree() { Value = model.DegreeEnum };
                user.Position = new Position() { Value = model.PositionEnum };
                user.AcademicTitle = new AcademicTitle() { Value = model.AcademicTitleEnum };
                user.ScholarLink = model.ScholarLink;
                user.OrcidLink = model.OrcidLink;
				if (model.FacultyId != null && model.DepartmentId != null)
				{
					using (var db = new ApplicationDbContext())
					{
						user.DepartmentUsers.Add(new DepartmentUser() { DepartmentId = model.DepartmentId, UserId = user.Id });
					}
				}

                if (model.ProfilePicture != null)
                {
                    byte[] image = new byte[model.ProfilePicture.ContentLength];
                    model.ProfilePicture.InputStream.Read(image, 0, Convert.ToInt32(model.ProfilePicture.ContentLength));
                    user.ProfilePicture = image;
                }

                IdentityResult result = UserManager.Update(user);
                if (result.Succeeded)
                {
					if (User.IsInRole("Admin"))
					{
						IdentityResult result2 = UserManager.RemoveFromRole(user.Id, UserManager.GetRoles(user.Id).FirstOrDefault());
						if (result2.Succeeded)
						{
							IdentityResult result3 = UserManager.AddToRole(user.Id, model.Role);

							if (result3.Succeeded)
							{
								AuthenticationManager.SignOut();
								return RedirectToAction("Login", "Account");
							}
							else
							{
								AddErrors(result3);
							}
						}
						else
						{
							AddErrors(result2);
						}
					}
					else
					{
						return RedirectToAction("Profile", "Home");
					}
									
                }
                else
                {
                    AddErrors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Profile", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Profile", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}