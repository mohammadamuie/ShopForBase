using Project.Application.DTOs.Base;
using System.Collections;
using System.Collections.Generic;

namespace Project.Application.DTOs.MenuItem
{
    public class MenuItemDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Url1 { get; set; }
        public bool ManualUrl { get; set; }
        public int? ParentId { get; set; }
        public string Parent { get; set; }
        public int ChildsCount { get; set; }
        public ICollection<MenuItemDTO> Childs { get; set; }
    }
}
