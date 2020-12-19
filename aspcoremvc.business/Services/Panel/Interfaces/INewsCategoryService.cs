using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface INewsCategoryService
    {
        NewsCategoryDto AddNewsCategory(NewsCategoryDto newsCategoryDto);

        NewsCategoryDto DeleteNewsCategory(int NewsCategoryId);

        NewsCategoryDto UpdateNewsCategory(NewsCategoryDto newsCategoryDto);

        NewsCategoryDto GetNewsCategoryById(int id);

        List<NewsCategoryDto> GetAllNewsCategories();

        NewsCategoryDto GetCategoryIdByCategoryName(string categoryName);





    }
}
