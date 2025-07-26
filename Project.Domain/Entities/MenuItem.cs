using Project.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool ManualUrl { get; set; }

        public int? ParentId { get; set; }

        public MenuItem Parent { get; set; }

        public ICollection<MenuItem> Childs { get; set; }

    }
}
