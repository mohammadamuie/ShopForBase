using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.DTOs.DataTable
{
    public class UserDataTable
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string Nationalcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedAt { get; set; }
    }
    public class UserDataTableInput : StringDatatableInput
    {
        
    }

}
