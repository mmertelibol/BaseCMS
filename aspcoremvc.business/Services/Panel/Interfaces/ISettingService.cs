using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface ISettingService
    {
        SettingDto AddSetting(SettingDto settingDto);
        SettingDto DeleteSetting(int SettingId);
        List<SettingDto> GetAllSettings();
        SettingDto GetSettingById(int SettingId);

        SettingDto UpdateSetting(SettingDto settingDto);
    }
}
