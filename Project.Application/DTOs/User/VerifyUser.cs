using DNTPersianUtils.Core;
using System.ComponentModel.DataAnnotations;
using Project.Application.Helpers;

namespace Project.Application.DTOs.User
{
    public class VerifyUser
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [ValidIranianMobileNumber(ErrorMessage = PublicHelper.PhoneValidationErrorMessage)]
        public string Phone { get; set; }

        [Display(Name = "کد تایید")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [Range(11111, 99999, ErrorMessage = PublicHelper.NotValidValidationErrorMessage)]
        public int VerificationCode { get; set; }

        public string ExpireSecondCode { get; set; }
    }
}
