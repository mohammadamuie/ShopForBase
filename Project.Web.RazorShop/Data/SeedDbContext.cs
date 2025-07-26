using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Application.Helpers;
using Project.Domain.Entities;
using Project.Domain.Enums;
using Project.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Data
{
    public class SeedDbContext
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //await roleManager.CreateAsync(new IdentityRole(PublicHelper.Roles.Administrator));
            //await roleManager.CreateAsync(new IdentityRole(PublicHelper.Roles.Customer));


            //var adminUserName = PublicHelper.Authorization.DefaultUserName;
            //var adminUser = new ApplicationUser
            //{
            //    AccessFailedCount = 0,
            //    Email = PublicHelper.Authorization.DefaultEmail,
            //    EmailConfirmed = true,
            //    FirstName = adminUserName,
            //    LastName = adminUserName,
            //    NormalizedUserName = adminUserName.Normalize(),
            //    UserName = adminUserName,
            //};
            //await userManager.CreateAsync(adminUser, PublicHelper.Authorization.DefaultPassword);
            //adminUser = await userManager.FindByNameAsync(adminUserName);
            //await userManager.AddToRoleAsync(adminUser, PublicHelper.Roles.Administrator);


        }
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
           
        }
    }
}
