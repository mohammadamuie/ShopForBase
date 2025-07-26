using DNTPersianUtils.Core;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.User
{
    public class RegisterCode
    {

        [Display(Name = "شمراه همراه")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [ValidIranianMobileNumber(ErrorMessage = PublicHelper.PhoneValidationErrorMessage)]
        public string PhoneNumber { get; set; }

        [Display(Name = "کد تایید")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Code { get; set; }

        public string ExpireSecondCode { get; set; }

        public string ReturnUrl { get; set; }
    }
}
