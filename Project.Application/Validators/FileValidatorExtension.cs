using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Project.Application.Validators
{
    public static class FileValidatorExtension
    {
        public static IRuleBuilder<T, IFormFile> IsValidImage<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("تصویر را وارد کنید")
                .Must(x => x.ContentType.Equals("image/jpeg") || x.ContentType.Equals("image/jpg") || x.ContentType.Equals("image/png")
                    || Path.GetExtension(x.FileName).ToLower() == ".jpg" || Path.GetExtension(x.FileName).ToLower() == ".png" || Path.GetExtension(x.FileName).ToLower() == ".jpeg")
                .WithMessage("تصویر ارسالی نامعتبر است");
        }

        public static IRuleBuilder<T, IFormFile> IsValidVideo<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("ویدیو را وارد کنید")
                .Must(x => x.ContentType.Equals("video/mp4") || x.ContentType.Equals("video/mpeg")
                    || Path.GetExtension(x.FileName).ToLower() == ".mp4" || Path.GetExtension(x.FileName).ToLower() == ".mpeg")
                .WithMessage("ویدیو ارسالی نامعتبر است");
        }

        public static IRuleBuilder<T, IFormFile> IsValidAudio<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("صوت را وارد کنید")
                .Must(x => x.ContentType.Equals("audio/aac") || x.ContentType.Equals("audio/mp3")
                    || Path.GetExtension(x.FileName).ToLower() == ".aac" || Path.GetExtension(x.FileName).ToLower() == ".mp3")
                .WithMessage("صوت ارسالی نامعتبر است");
        }
    }
}
