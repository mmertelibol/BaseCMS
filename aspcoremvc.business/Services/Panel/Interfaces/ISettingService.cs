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
      
        SettingDto UpdateSetting(SettingDto settingDto);

        SettingDto GetSetting();
    }

}
