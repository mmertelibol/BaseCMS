﻿using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain.Panel;
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

        public NewsCategoryDto DeleteNewsCategory(int NewsCategoryId)
        {
            var newsCategory = _context.NewsCategory.Find(NewsCategoryId);
            var deletedNewsCategory = _context.NewsCategory.Remove(newsCategory);

            var dtoModel = _mapper.Map<NewsCategoryDto>(deletedNewsCategory);

            _context.SaveChanges();

            return dtoModel;
        }

        public List<NewsCategoryDto> GetAllNewsCategories()
        {
            var NewsCategoryList = _context.NewsCategory.Where(x => x.IsDeleted == false).ToList();

            var dtoModel = _mapper.Map<List<NewsCategoryDto>>(NewsCategoryList);

            return dtoModel;
        }

        public NewsCategoryDto GetCategoryIdByCategoryName(string categoryName)
        {
            var categoryId = _context.NewsCategory.Where(x => x.Name == categoryName).FirstOrDefault().Id;

            var dtomodel = _mapper.Map<NewsCategoryDto>(categoryId);

            return dtomodel;


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
