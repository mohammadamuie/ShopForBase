using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.User;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IIdentityUserService
    {
        Task<UserProfile> GetProfile(string UserName);
        Task<DatatableResponse<UserDataTable>> GetDataTable(UserDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task SignIn(SignInUser signup);
        Task Verify(RegisterCode input);
        Task<string> GetUserExpireCodeTime(string Phonenumber);
        Task<IEnumerable<SelectListItem>> GetSelectOptionData();
        Task<UserProfile> GetProfile();
        Task EditProfile(EditProfile input);
        Task<string> SendCode();
        Task<ApplicationUser> CurrentLoginDTO();
        Task EditProfileWithOutPhoneNumber(EditProfileWithOutPhoneNumber input);
    }
}
