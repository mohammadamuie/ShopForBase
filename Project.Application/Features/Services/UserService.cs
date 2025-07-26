using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Models;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ISmsSender _smsSender;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings, IMapper mapper, ISmsSender smsSender, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _smsSender = smsSender;
            _userRepository = userRepository;
        }

    }
}
