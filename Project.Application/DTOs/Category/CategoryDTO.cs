using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Category
{
    public class CategoryDTO : BaseDTO
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool? Special { get; set; }
    }
}
