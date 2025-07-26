using System.ComponentModel.DataAnnotations;
using DNTPersianUtils.Core;
using Project.Application.Helpers;

namespace Project.Application.DTOs.User
{
    public class AuthenticateUser
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [ValidIranianMobileNumber(ErrorMessage = PublicHelper.PhoneValidationErrorMessage)]
        public string Phone { get; set; }

        public int? RefferId { get; set; }
    }
}
