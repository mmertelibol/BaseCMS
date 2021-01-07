using Common.Dto.PanelDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Panel.Interfaces
{
    public interface ISettingService
    {
       //SettingDto AddSetting(SettingDto settingDto/*,IFormFile file*/);
        //SettingDto DeleteSetting(int SettingId);
        //List<SettingDto> GetAllSettings();
        //SettingDto GetSettingById(int SettingId);

        SettingDto UpdateSetting(SettingDto settingDto);

        SettingDto GetSetting();
    }

}
