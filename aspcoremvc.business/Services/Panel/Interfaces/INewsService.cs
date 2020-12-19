using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface INewsService
    {
        List<NewsDto> GetAllNews();

        NewsDto GetNewsById(int id);

        NewsDto AddNews(NewsDto newsDto);

        NewsDto DeleteNews(int id);

        NewsDto UpdateNews(NewsDto newsDto);

        int GetLastNewsId();




    }
}
