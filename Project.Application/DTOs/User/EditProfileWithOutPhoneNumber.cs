using System.ComponentModel.DataAnnotations;
using Project.Application.Helpers;

namespace Project.Application.DTOs.User
{
    public class EditProfileWithOutPhoneNumber
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [EmailAddress(ErrorMessage = PublicHelper.NotValidValidationErrorMessage)]
        public string Email { get; set; }


        [Display(Name = "آدرس")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Adress { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Postalcode { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Nationalcode { get; set; }

        //[Display(Name = "تاریخ تولد")]
        //[Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string DateOfBirthDay { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Province { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string City { get; set; }
    }
}
