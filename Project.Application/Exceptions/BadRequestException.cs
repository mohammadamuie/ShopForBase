using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException() : base("خطا در ثبت اطلاعات")
        {
        }
    }
}
