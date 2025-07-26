using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Pages
{
    public class PagesDTO : BaseDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }


        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyWords { get; set; }

    }
}
