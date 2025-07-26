using System.Threading.Tasks;
using Project.Application.Models;

namespace Project.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
