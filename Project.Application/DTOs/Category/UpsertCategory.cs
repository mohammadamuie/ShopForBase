using Microsoft.AspNetCore.Http;
using Project.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Category
{
    public class UpsertCategory
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }

        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
