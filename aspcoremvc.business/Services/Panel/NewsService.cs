using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain.Panel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public NewsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public NewsDto AddNews(NewsDto newsDto)
        {
            newsDto.AddedDate = DateTime.Now;
            var news = _mapper.Map<News>(newsDto);
            var addedNews = _context.News.Add(news);
            var newsDtoModel = _mapper.Map<NewsDto>(addedNews);

            _context.SaveChanges();

            return newsDtoModel;


        }

        public NewsDto DeleteNews(int id)
        {

            var news = _context.News.Where(x => x.Id == id);
            var deletedNews = _context.Remove(news);

            var NewsDtoModel = _mapper.Map<NewsDto>(deletedNews);

            _context.SaveChanges();

            return NewsDtoModel;

        }

        public List<NewsDto> GetAllNews()
        {
            var newsList = _context.News.Include(x => x.NewsCategory).Where(x => x.IsDeleted == false).ToList();
            var newsListDtoModel = _mapper.Map<List<NewsDto>>(newsList);

            return newsListDtoModel;
        }

        public NewsDto GetNewsById(int newsId)
        {
            var news = _context.News.Find(newsId);
            var newsDtoModel = _mapper.Map<NewsDto>(news);

            return newsDtoModel;

        }

        public NewsDto UpdateNews(NewsDto newsDto)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == newsDto.Id);

            news.Id = newsDto.Id;
            news.HTMLContent = newsDto.HTMLContent;
            news.MetaDescription = newsDto.MetaDescription;
            news.MetaKeywords = newsDto.MetaKeywords;
            news.NewsCategoryId = newsDto.NewsCategoryId;
            news.Author = newsDto.Author;
            news.RoutingUrl = newsDto.RoutingUrl;
            news.ViewCount = newsDto.ViewCount;
            news.UpdatedDate = DateTime.Now;

            _context.Update(news);
            _context.SaveChanges();

            return newsDto;

        }
    }
}
