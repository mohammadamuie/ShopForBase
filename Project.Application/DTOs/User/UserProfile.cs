using Project.Application.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.Application.DTOs.User
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfBirthForShow { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string Nationalcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirthDay { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string ExpireSecondCode { get; set; }
    }
}
