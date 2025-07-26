using Project.Application.DTOs.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface ISettingService
    {
        Task<double> GetSendPrice();
        Task<SettingDTO> GetSetting();
        Task UpdateSettings(UpsertSetting input);
    }
}
