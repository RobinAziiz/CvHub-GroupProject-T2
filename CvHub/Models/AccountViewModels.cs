using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Data.Models;

namespace CvHub.Models
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
        
    }

    public class RegisterCVViewModel
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Phonenumber")]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "Adress")]
        [Required]
        public string Adress { get; set; }

        [Display(Name = "BirthDate")]
        [Required]
        public string BirthDate { get; set; }
        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }
        [Display(Name = "Bio")]
        [Required]
        [StringLength(400)]
        public string Bio { get; set; }
        [Display(Name = "Profession")]
        [Required]
        public string Profession { get; set; }
        [Display(Name = "List your skills")]
        
        public List<Skill> Skill { get; set; }
        [Display(Name = "Make Profile only visible for logged in users?")]
        public bool SetPrivate { get; set; }
        [Required]
        public HttpPostedFileBase Image { get; set; }
        public int Id { get; set; }
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
