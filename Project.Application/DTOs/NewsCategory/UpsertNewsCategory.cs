using Project.Application.Helpers;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.NewsCategory
{
    public class UpsertNewsCategory
    {
        public int? Id { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Name { get; set; }

    }
}
