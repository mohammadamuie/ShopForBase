using Project.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Banner : BaseEntity
    {
        public string Url { get; set; }
        public string Image { get; set; }
    }
}
