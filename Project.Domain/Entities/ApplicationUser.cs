using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {

        [StringLength(maximumLength: 100)]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 100)]
        public string LastName { get; set; }
        public ApplicationUser()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsActive = true;
        }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpireCode { get; set; }
        public string Code { get; set; }
        public int? ProfileId { get; set; }
    }
}
