using System.Dynamic;

namespace Project.Application.Models
{
    public class SmsWithPattern
    {
        public string To { get; set; }
        public string PatternCode { get; set; }
        public ExpandoObject PatternData { get; set; }
    }
}
