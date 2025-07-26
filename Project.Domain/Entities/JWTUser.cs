using System;
using System.Collections.Generic;
using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class JWTUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GetNickName() => $"{FirstName} {LastName}".Trim();
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public int VerificationCode { get; set; } = 0;
    }
}