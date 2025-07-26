using DNTPersianUtils.Core;
using Project.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.User
{
    public class SignInUser
    {
        [Display(Name = "شمراه همراه")]
        [Required(ErrorMessage =PublicHelper.RequiredValidationErrorMessage)]
        [ValidIranianMobileNumber(ErrorMessage =PublicHelper.PhoneValidationErrorMessage)]
        public string  PhoneNumber { get; set; }
    }
}
