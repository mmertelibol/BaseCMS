using Common.Dto.PanelDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface IStaticPageService
    {
        List<StaticPageDto> GetAllStaticPage();
        StaticPageDto GetStaticPageById(int id);

        StaticPageDto AddStaticPage(StaticPageDto staticPageDto);

        StaticPageDto DeleteStaticPage(int id);

        StaticPageDto UpdateStaticPage(StaticPageDto staticPageDto);
    }
}
