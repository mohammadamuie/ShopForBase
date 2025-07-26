using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Base
{
    public class MetaBaseModel : BaseEntity
    {
        public string MetaTitle { get; set; }
        public string MetaKeyWord { get; set; }
        public string MetaDescription { get; set; }
    }
}
