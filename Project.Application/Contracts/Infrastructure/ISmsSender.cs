using System.Threading.Tasks;
using Project.Application.Models;

namespace Project.Application.Contracts.Infrastructure
{
    public interface ISmsSender
    {

        Task<bool> SendWithPattern(SmsWithPattern smsWithPattern);
        Task SendWithPatternAsync(string phone, string patternCode, string data);
        bool SimpleSend(SimpleSms simpleSms);
        bool SimpleSend(string to, string content);
    }
}
