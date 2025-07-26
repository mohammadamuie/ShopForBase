using Project.Domain.Entities.Base;
using System.Net;

namespace Project.Domain.Entities.News
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
