using Project.Domain.Entities.Base;
using System;

namespace Project.Domain.Entities
{
    public class UserInformation : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string Nationalcode { get; set; }
        public string Email { get; set; }
        public string DateOfBirthDay { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string UserId { get; set; }
    }
}
