using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface ISocialMediaService
    {
        List<SocialMediaDto> GetAllList();
        SocialMediaDto GetSocialMediaById(int id);

        SocialMediaDto AddSocialMedia(SocialMediaDto socialMediaDto);

        SocialMediaDto DeleteSocialMedia(int id);

        SocialMediaDto UpdateSocialMedia(SocialMediaDto socialMediaDto);

      


    }
}
