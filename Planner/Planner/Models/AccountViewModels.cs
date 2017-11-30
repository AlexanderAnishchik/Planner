using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Planner.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
         [Required]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "По батькові")]
        public string ThirdName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль повинен мати не менше {2} символів.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пiдтвердiть пароль")]
        [Compare("Password", ErrorMessage = "Пароль та пiдтвердження паролю не співпадає")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Вчена ступінь")]
        [Range(1, int.MaxValue, ErrorMessage = "Виберiть вчену ступiнь")]
        public AcademicTitleEnum AcademicTitleEnum { get; set; }
        [Display(Name = "Вчене звання")]
        [Range(1, int.MaxValue, ErrorMessage = "Виберiть вчене звання")]
        public DegreeEnum DegreeEnum { get; set; }
        [Display(Name = "Посада")]
        [Range(1, int.MaxValue, ErrorMessage = "Виберiть посаду")]
        public PositionEnum PositionEnum { get; set; }
		[Display(Name = "Профiль у Google Scholar")]
		public string ScholarLink { get; set; }
		[Display(Name = "Профiль у ORCID")]
		public string OrcidLink { get; set; }
        [Display(Name = "Роль")]
        public string Role { get; set; }
		public string DepartmentId { get; set; }
	}

    public class GetUsersModel
    {
        [Display(Name = "UserList")]
        public List<EditingUser> UserList { get; set; }
    }

	public class EditingUser
	{
		public String Id { get; set; }
		public String Name { get; set; }
		public String PositionId { get; set; }
		public String Email { get; set; }
        public bool IsActive { get; set; }
	}

    public class EditModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "По батькові")]
        public string ThirdName { get; set; }

        [Display(Name = "Аватар")]
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Display(Name = "Вчена ступінь")]
        [Range(1, int.MaxValue, ErrorMessage = "Виберiть вчену ступiнь")]
        public AcademicTitleEnum AcademicTitleEnum { get; set; }
        [Display(Name = "Вчене звання")]
        [Range(1, int.MaxValue, ErrorMessage = "Виберiть вчене звання")]
        public DegreeEnum DegreeEnum { get; set; }
        [Display(Name = "Посада")]
        [Range(1, int.MaxValue, ErrorMessage = "Виберiть посаду")]
        public PositionEnum PositionEnum { get; set; }
        [Display(Name = "Профiль у Google Scholar")]
        public string ScholarLink { get; set; }
        [Display(Name = "Профiль у ORCID")]
        public string OrcidLink { get; set; }
		[Display(Name = "Роль")]
		public string Role { get; set; }
		public string FacultyId { get; set; }
		public string DepartmentId { get; set; }
	}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
