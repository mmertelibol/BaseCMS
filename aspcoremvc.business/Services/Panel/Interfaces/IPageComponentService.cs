using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface IPageComponentService
    {

        List<PageComponentDto> GetAllPageComponents();
        PageComponentDto GetPageComponentById(int id);

        PageComponentDto DeletePageComponent(int id);

        PageComponentDto AddPageComponent(PageComponentDto pageComponentDto);

        PageComponentDto UpdatePageComponent(PageComponentDto pageComponentDto);
    }
}
