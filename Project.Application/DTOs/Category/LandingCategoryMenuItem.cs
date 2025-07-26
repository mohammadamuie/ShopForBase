using System.Collections.Generic;

namespace Project.Application.DTOs.Category
{
    public class LandingCategoryMenuItem
    {
        public bool IsRoot { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public List<LandingCategoryMenuItem> Childs { get; set; }
    }
}
