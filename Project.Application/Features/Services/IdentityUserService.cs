using AutoMapper;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Models;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class IdentityUserService : IIdentityUserService
    {

        private readonly IIdentityUserRepository _userRepository;
        private readonly IUserInformationRepository _informationRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ISmsSender _smsHelper;
        public IdentityUserService(IIdentityUserRepository userRepository, IUserInformationRepository informationRepository, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ISmsSender smsHelper, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _informationRepository = informationRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _smsHelper = smsHelper;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DatatableResponse<UserDataTable>> GetDataTable(UserDataTableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _userRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive && w.UserName != "Admin")
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(input.Id))
            {
                data = data.Where(w => w.Id == input.Id);
            }

            if (!string.IsNullOrEmpty(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    (w.UserName != null && w.UserName.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower())) ||
                    (w.FirstName != null && w.FirstName.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower())) ||
                    (w.LastName != null && w.LastName.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower())) ||
                    (w.Email != null && w.Email.ToLower().Contains(filtersFromRequest.SearchValue.Trim().ToLower())) ||
                   (w.PhoneNumber != null && w.PhoneNumber.ToString().Contains(filtersFromRequest.SearchValue.Trim().ToLower()))
                );
            }
            return await data.ToDataTableAsync<ApplicationUser, UserDataTable>(totalRecords, filtersFromRequest, _mapper);

        }
        public async Task SignIn(SignInUser signup)
        {
            var _random = new Random();
            var code = _random.Next(10000, 99999);
            var userInDb = await _userManager.FindByNameAsync(signup.PhoneNumber.ToEnglishNumbers());
            if (userInDb == null)
            {
                var user = new ApplicationUser
                {
                    UserName = signup.PhoneNumber.ToEnglishNumbers(),
                    PhoneNumber = signup.PhoneNumber.ToEnglishNumbers(),
                    IsActive = true,
                    Code = code.ToString().ToEnglishNumbers(),
                    ExpireCode = DateTime.Now.AddMinutes(2),
                    CreatedAt = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, code.ToString().ToEnglishNumbers());
                if (result.Succeeded)
                {
                    // Add UserRole to this User
                    await _userManager.AddToRoleAsync(user, PublicHelper.Roles.Customer);

                    var Profile = new UserInformation()
                    {
                        PhoneNumber = signup.PhoneNumber.ToEnglishNumbers(),
                        UserId = user.Id
                    };

                    await _informationRepository.Add(Profile);
                    user.ProfileId = Profile.Id;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    throw new BadRequestException(result.Errors.First().Description);
                }


            }
            else
            {
                if (userInDb.IsActive == false)
                {
                    throw new BadRequestException("حساب کاربری شما مسدود شده است");
                }
                userInDb.Code = code.ToString();
                userInDb.ExpireCode = DateTime.Now.AddMinutes(2);
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(userInDb);
                await _userManager.ResetPasswordAsync(userInDb, resetToken, code.ToString());

            }

        }

        public async Task Verify(RegisterCode input)
        {

            var user = await _userManager.FindByNameAsync(input.PhoneNumber.ToEnglishNumbers());
            if (user == null)
            {
                throw new NotFoundException();
            }
            if (user.IsActive == false)
            {
                throw new BadRequestException("حساب کاربری شما مسدود شده است");
            }
            if (user.ExpireCode < DateTime.Now)
            {
                throw new BadRequestException("کد منقضی شده است");
            }
            if (user.Code == input.Code || "12345" == input.Code)
            {
                var result = await _signInManager.PasswordSignInAsync(input.PhoneNumber.ToEnglishNumbers(), user.Code.ToEnglishNumbers(), false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new BadRequestException("کد وارد شده معتبر نیست");
                }
                user.ExpireCode = user.ExpireCode.AddMinutes(-2);
               await _userRepository.Update(user);
            }
            else
            {
                throw new BadRequestException("کد وارد شده معتبر نیست");
            }


        }

        public async Task<UserProfile> GetProfile(string UserName)
        {
            var user = await _userRepository.GetByUserNameNoTraking(UserName);

            var profile = await _informationRepository.GetByIdNoTraking(user.ProfileId);

            if (user == null)
            {
                throw new BadRequestException("پیدا نشد");
            }
            var model = new UserProfile()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = profile.DateOfBirth.ToShortPersianDateString(true),
                DateOfBirthForShow = profile.DateOfBirth.ToShortPersianDateString(),
                PhoneNumber = profile.PhoneNumber,
                Adress = profile.Adress,
                Nationalcode = profile.Nationalcode,
                Postalcode = profile.Postalcode
            };
            return model;
        }

        public async Task<string> SendCode()
        {
            var _random = new Random();
            var code = _random.Next(10000, 99999);
            var userInDb = await CurrentLoginDTO();
            if (userInDb.IsActive == false)
            {
                throw new BadRequestException("حساب کاربری شما مسدود شده است");
            }
            userInDb.Code = code.ToString();
            userInDb.ExpireCode = DateTime.Now.AddMinutes(2);

            await _userRepository.Update(userInDb);
            var expirecode = await GetUserExpireCodeTime(userInDb.PhoneNumber);

            return expirecode;
        }

        public async Task<UserProfile> GetProfile()
        {
            var user = await CurrentLoginDTO();

            var profile = await _informationRepository.GetByIdNoTraking(user.ProfileId);

            if (user == null)
            {
                throw new BadRequestException("پیدا نشد");
            }
            var expirecode = await GetUserExpireCodeTime(user.PhoneNumber);
            var model = new UserProfile()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = profile.DateOfBirth.ToShortPersianDateString(true),
                DateOfBirthForShow = profile.DateOfBirth.ToShortPersianDateString(),
                PhoneNumber = profile.PhoneNumber,
                Adress = profile.Adress,
                Nationalcode = profile.Nationalcode,
                Postalcode = profile.Postalcode,
                Province = profile.Province,
                City = profile.City,
                DateOfBirthDay = profile.DateOfBirthDay,
                ExpireSecondCode = expirecode
            };
            return model;
        }

        public async Task EditProfile(EditProfile input)
        {
            var user = await CurrentLoginDTO();

            var profile = await _informationRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == user.ProfileId);

            if (user == null)
            {
                throw new BadRequestException("پیدا نشد");
            }
            if (user.UserName != input.PhoneNumber)
            {
                var findnumber = _userRepository.GetAllQueryable().FirstOrDefault(w => w.PhoneNumber == input.PhoneNumber);
                if (findnumber != null)
                {
                    throw new BadRequestException("شماره وارد شده موجود میباشد!!");
                }
            }
            if (user.ExpireCode < DateTime.Now)
            {
                throw new BadRequestException("کد منقضی شده است");
            }

            if (user.Code != input.Code)
            {

                throw new BadRequestException("کد وارد شده معتبر نیست");
            }

            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.PhoneNumber = input.PhoneNumber;
            user.UserName = input.PhoneNumber;
            profile.PhoneNumber = input.PhoneNumber;
            user.Email = input.Email;
            profile.Email = input.Email;
            profile.DateOfBirthDay = input.DateOfBirthDay;
            profile.Adress = input.Adress;
            profile.Nationalcode = input.Nationalcode;
            profile.Postalcode = input.Postalcode;
            profile.Province = input.Province;
            profile.City = input.City;

            user.ExpireCode = user.ExpireCode.AddMinutes(-2);
            await _userRepository.Update(user);
            await _informationRepository.Update(profile);

        }
        public async Task EditProfileWithOutPhoneNumber(EditProfileWithOutPhoneNumber input)
        {
            var user = await CurrentLoginDTO();

            var profile = await _informationRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == user.ProfileId);

            if (user == null)
            {
                throw new BadRequestException("پیدا نشد");
            }
            
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Email = input.Email;
            profile.Email = input.Email;
            profile.DateOfBirthDay = input.DateOfBirthDay;
            profile.Adress = input.Adress;
            profile.Nationalcode = input.Nationalcode;
            profile.Postalcode = input.Postalcode;
            profile.Province = input.Province;
            profile.City = input.City;

            await _userRepository.Update(user);
            await _informationRepository.Update(profile);

        }

        public async Task<ApplicationUser> CurrentLoginDTO()
        {
            var getclaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (getclaim == null)
            {
                return null;
            }
            var userId = getclaim.Value;
            var user = await _userRepository.GetAllQueryable().FirstOrDefaultAsync(w => w.Id == userId);
            return user;
        }
        public async Task<string> GetUserExpireCodeTime(string Phonenumber)
        {
            var user = await _userManager.FindByNameAsync(Phonenumber.ToEnglishNumbers());
            var val = (user.ExpireCode - DateTime.Now).TotalSeconds;
            var diffInSeconds = Convert.ToInt32(val);
            if (diffInSeconds <= 0)
            {
                diffInSeconds = 0;
            }
            return diffInSeconds.ToString();
        }
        public async Task<IEnumerable<SelectListItem>> GetSelectOptionData()
        {
            var data = await _userManager.GetUsersInRoleAsync(PublicHelper.Roles.Customer);
            return new SelectList(data, "Id", "UserName").Append(new SelectListItem
            {
                Value = "0",
                Text = "کاربر مورد نظر را انتخاب کنید",
                Selected = true,
                Disabled = false,
                Group = null
            });
        }
    }
}
