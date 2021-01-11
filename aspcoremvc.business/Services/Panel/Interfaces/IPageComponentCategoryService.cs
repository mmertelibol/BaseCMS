using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface IPageComponentCategoryService
    {
        List<PageComponentCategoryDto> GetAllPageComponentCategories();
        PageComponentCategoryDto GetPageComponentCategoryById(int id);

        PageComponentCategoryDto DeletePageComponentCategory(int id);

        PageComponentCategoryDto AddPageComponentCategory(PageComponentCategoryDto dto);

        PageComponentCategoryDto UpdatePageComponentCategory(PageComponentCategoryDto dto);

        PageComponentCategoryDto DeleteAndAssignComponentCategory(PageComponentCategoryDto dto);
    }
}
