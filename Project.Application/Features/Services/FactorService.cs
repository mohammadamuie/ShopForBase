using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Factor;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class FactorService : IFactorService
    {
        private readonly IFactorRepository _factorRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityUserService _identityUserService;

        public FactorService(IFactorRepository factorRepository, IMapper mapper, IIdentityUserService identityUserService)
        {
            _factorRepository = factorRepository;
            _mapper = mapper;
            _identityUserService = identityUserService;
        }


        public async Task<FactorDTO> Create(CreateFactor input)
        {
            var model = _mapper.Map<Factor>(input);

            var user = await _identityUserService.CurrentLoginDTO();
            model.UserId = user.Id;
            model = await _factorRepository.Add(model);

            return _mapper.Map<FactorDTO>(model);
        }

        public async Task<FactorDTO> GetById(int id)
        {
            var find = await _factorRepository.GetNoTracking(id);

            var user = await _identityUserService.CurrentLoginDTO();
            var UserId = user.Id;

            if (find == null || find.UserId != UserId)
            {
                return null;
            }

            return _mapper.Map<FactorDTO>(find);
        }

        public async Task<bool> Delete(int id)
        {
            await _factorRepository.Delete(id);

            return true;
        }

        public async Task<bool> Recover(int id)
        {
            await _factorRepository.Recover(id);

            return true;
        }
    }
}
