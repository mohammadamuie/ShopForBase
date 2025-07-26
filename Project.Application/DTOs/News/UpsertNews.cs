using Microsoft.AspNetCore.Http;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.News
{
    public class UpsertNews
    {
        public int? Id { get; set; }

        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Date { get; set; }
        
        [Display(Name = "ساعت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Time { get; set; }

        [Display(Name = "عنوان خبر & مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
       
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortContent { get; set; }
        
        [Display(Name = "شرح خبر & مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Content { get; set; }
        
        [Display(Name = "تگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; }
        
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Url { get; set; }
        
        [Display(Name = "دسته بندی خبر & مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }
        
        [Display(Name = "تصویر خبر & مقاله")]
        public IFormFile Image { get; set; }
        
        [Display(Name = "متا توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MetaDescription { get; set; }
        
        [Display(Name = "عنوان متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MetaTitle { get; set; }
        
        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MetaKeywords { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
