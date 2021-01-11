using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
using Data;
using Data.Domain.Panel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public NewsCategoryService(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public NewsCategoryDto AddNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            newsCategoryDto.AddedDate = DateTime.Now;
            var newsCategory = _mapper.Map<NewsCategory>(newsCategoryDto);
            var addedNewsCategory = _context.NewsCategory.Add(newsCategory);
            var dtoModel = _mapper.Map<NewsCategoryDto>(newsCategory);

            _context.SaveChanges();

            return dtoModel;

        }

        public NewsCategoryDto DeleteAndAssignNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newsList = _context.News
                        .Where(x => x.NewsCategoryId == newsCategoryDto.Id)
                        .ToList();
                    foreach (var item in newsList)
                    {
                        if (item.NewsCategoryId!= newsCategoryDto.NewsCategoryId)
                        {
                            item.NewsCategoryId = newsCategoryDto.NewsCategoryId;
                            item.UpdatedDate = DateTime.Now;


                            _context.Update(item);
                            _context.SaveChanges();
                        }
                           
                        
                       
                    }
                    var deletedCategory = _context.NewsCategory
                        .Find(newsCategoryDto.Id);
                    _context.Remove(deletedCategory);
                    _context.SaveChanges();



                    transaction.Commit();


                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return newsCategoryDto;
        }

        public NewsCategoryDto DeleteNewsCategory(int newsCategoryId)
        {

            //IsValidForDelete(newsCategoryId);
            var newsCategory = _context.NewsCategory.Find(newsCategoryId);
            var deletedNewsCategory = _context.NewsCategory.Remove(newsCategory);
            var dtoModel = _mapper.Map<NewsCategoryDto>(newsCategory);
            _context.SaveChanges();

            return dtoModel;

             
        }
        public List<NewsCategoryDto> GetAllNewsCategories()
        {
            var newsCategoryList = _context.NewsCategory.Where(x => x.IsDeleted == false).ToList();

            var dtoModel = _mapper.Map<List<NewsCategoryDto>>(newsCategoryList);

            return dtoModel;
        }

       
        public NewsCategoryDto GetNewsCategoryById(int id)
        {
            var newsCategory = _context.NewsCategory.Find(id);
            var dtoModel = _mapper.Map<NewsCategoryDto>(newsCategory);

            return dtoModel;
        }

      

        public NewsCategoryDto UpdateNewsCategory(NewsCategoryDto newsCategoryDto)
        {
            var newsCategory = _context.NewsCategory.Find(newsCategoryDto.Id);

            newsCategory.Id = newsCategoryDto.Id;
            newsCategory.Name = newsCategoryDto.Name;
            newsCategory.UpdatedDate = DateTime.Now;

            _context.Update(newsCategory);
            _context.SaveChanges();

            return newsCategoryDto;



        }
    }
}
