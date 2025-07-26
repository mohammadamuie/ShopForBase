using Project.Application.DTOs.Factor;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IFactorService
    {
        Task<FactorDTO> Create(CreateFactor input);
        Task<bool> Delete(int id);
        Task<FactorDTO> GetById(int id);
        Task<bool> Recover(int id);
    }
}
