using System;

namespace Project.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string item) : base($"{item} پیدا نشد")
        {
        }
        public NotFoundException() : base("اطلاعات مورد نظر پیدا نشد")
        {
        }
    }
}
