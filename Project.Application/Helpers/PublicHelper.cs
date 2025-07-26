using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Project.Application.Helpers
{
    public static class PublicHelper
    {
        public class Authorization
        {
            public const string DefaultUserName = "admin";
            public const string DefaultEmail = "admin@gmail.com";
            public const string DefaultPassword = "asdfghjkl;'";
        }
        public class Roles
        {
            public const string Administrator = "SuperAdmin";
            public const string AdministratorScheme = "SuperAdminScheme";
            public const string Customer = "Customer";
            public const string CustomerScheme = "CustomerScheme";
        }
        public const string SessionCaptcha = "_Captcha";
        public const string RequiredValidationErrorMessage = "{0} را وارد نکرده اید";
        public const string NotValidValidationErrorMessage = "{0} نامعتبر است";
        public const string PhoneValidationErrorMessage = "شمراه همراه نامعتبر است";

        private static bool IsValid(IFormFile image)
        {
            string mimeType = image.ContentType.ToLower();
            if (mimeType != "image/jpg" &&
                 mimeType != "image/jpeg" &&
                 mimeType != "image/png")
            {
                return false;
            }

            return true;
        }
        public static string FilterUrl(string Url)
        {
            var m = Url.Replace(" ", "_");
            m = m.Replace("‌", "_");
            m = m.Replace("@", "_");
            m = m.Replace("!", "_");
            m = m.Replace("$", "_");
            m = m.Replace("%", "_");
            m = m.Replace("^", "_");
            m = m.Replace("&", "_");
            m = m.Replace("*", "_");
            m = m.Replace("(", "_");
            m = m.Replace(")", "_");
            m = m.Replace("+", "_");
            m = m.Replace("=", "_");
            m = m.Replace("-", "_");
            m = m.Replace(":", "_");
            m = m.Replace(";", "_");
            m = m.Replace("'", "_");
            m = m.Replace("?", "_");
            m = m.Replace("/", "_");
            m = m.Replace(".", "_");
            m = m.Replace("<", "_");
            m = m.Replace(">", "_");


            return m;
        }
        public static bool IsVideoMimeTypeValid(IFormFile image)
        {
            string mimeType = image.ContentType.ToLower();
            if (mimeType != "image/jpg" &&
                 mimeType != "image/jpeg" &&
                 mimeType != "image/pjpeg" &&
                 mimeType != "image/gif" &&
                 mimeType != "image/x-png" &&
                 mimeType != "image/png")
            {
                return false;
            }
            return true;
        }

        public static bool IsSoundMimeTypeValid(IFormFile image)
        {
            string mimeType = image.ContentType.ToLower();
            if (mimeType != "image/jpg" &&
                 mimeType != "image/jpeg" &&
                 mimeType != "image/pjpeg" &&
                 mimeType != "image/gif" &&
                 mimeType != "image/x-png" &&
                 mimeType != "image/png")
            {
                return false;
            }
            return true;
        }

        private static readonly Random random = new();

        public static int GetRandomInt()
        {
            int from = 11111, to = 99999;
            return random.Next(from, to);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static string NumberFormat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            if (decimal.TryParse(input, out decimal number))
            {
                return String.Format("{0:n0}", number);
            }

            return "";
        }
        public static string GetDisplayAttributeFrom(this Enum enumValue)
        {
            MemberInfo info = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First();
            if (info != null && info.CustomAttributes.Any())
            {
                DisplayAttribute nameAttr = info.GetCustomAttribute<DisplayAttribute>();
                return nameAttr != null ? nameAttr.Name : enumValue.ToString();
            }
            return enumValue.ToString();
        }
    }
}