using System;
using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.User
{
    public class UserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }

        public int? RefferId { get; set; }
        public string Reffer { get; set; }

        public int OldId { get; set; }
        public int OldAgencyId { get; set; }
        public int OldVisitorId { get; set; }

        public string GetNickName() => $"{FirstName} {LastName}".Trim();
    }
}
