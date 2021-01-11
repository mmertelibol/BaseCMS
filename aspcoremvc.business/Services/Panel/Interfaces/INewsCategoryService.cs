using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface INewsCategoryService
    {
        NewsCategoryDto AddNewsCategory(NewsCategoryDto newsCategoryDto);

        NewsCategoryDto DeleteNewsCategory(int newsCategoryId);

        NewsCategoryDto UpdateNewsCategory(NewsCategoryDto newsCategoryDto);

        NewsCategoryDto GetNewsCategoryById(int id);

        List<NewsCategoryDto> GetAllNewsCategories();

        NewsCategoryDto DeleteAndAssignNewsCategory(NewsCategoryDto newsCategoryDto);

        List<NewsCategoryDto> GetCategoriesWithoutDeletedCategory(int id);





      





    }
}
