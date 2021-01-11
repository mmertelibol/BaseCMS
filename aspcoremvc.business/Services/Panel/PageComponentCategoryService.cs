using AutoMapper;
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
    public class PageComponentCategoryService:IPageComponentCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PageComponentCategoryService(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public PageComponentCategoryDto AddPageComponentCategory(PageComponentCategoryDto dto)
        {
            dto.AddedDate = DateTime.Now;
            var pageComponentCategory = _mapper.Map<PageComponentCategory>(dto);
             _context.PageComponentCategory.Add(pageComponentCategory);

            //var dtoModel = _mapper.Map<PageComponentCategoryDto>(pageComponentCategory);

            _context.SaveChanges();
            return dto;


        }

        public PageComponentCategoryDto DeleteAndAssignComponentCategory(PageComponentCategoryDto dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newsList = _context.PageComponent
                        .Where(x => x.PageComponentCategoryId == dto.Id)
                        .ToList();
                    foreach (var item in newsList)
                    {
                        if (item.PageComponentCategoryId != dto.PageComponentCategoryId)
                        {
                            item.PageComponentCategoryId = dto.PageComponentCategoryId;
                            item.UpdatedDate = DateTime.Now;

                            _context.Update(item);
                            _context.SaveChanges();
                        }
                    }
                    var deletedCategory = _context.PageComponentCategory
                        .Find(dto.Id);
                    _context.Remove(deletedCategory);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return dto;
        }

        public PageComponentCategoryDto DeletePageComponentCategory(int id)
        {
            var pageComponentCategory = _context.PageComponentCategory.Find(id);

            var deletedComponentCategory = _context.PageComponentCategory.Remove(pageComponentCategory);
            var dtoModel = _mapper.Map<PageComponentCategoryDto>(pageComponentCategory);

            _context.SaveChanges();

            return dtoModel;
        }

        public List<PageComponentCategoryDto> GetAllPageComponentCategories()
        {
            var pageComponentCategoriesList = _context.PageComponentCategory.Where(x => x.IsDeleted == false).ToList();
            var dtoModel = _mapper.Map<List<PageComponentCategoryDto>>(pageComponentCategoriesList);

            return dtoModel;
        }

        public PageComponentCategoryDto GetPageComponentCategoryById(int id)
        {
            var pageComponentCategory = _context.PageComponentCategory.Find(id);

            var dtoModel = _mapper.Map<PageComponentCategoryDto>(pageComponentCategory);

            return dtoModel;
        }

        public PageComponentCategoryDto UpdatePageComponentCategory(PageComponentCategoryDto dto)
        {
            var pageComponentCategory = _context.PageComponentCategory.Find(dto.Id);
            pageComponentCategory.Id = dto.Id;
            pageComponentCategory.Name = dto.Name;
            pageComponentCategory.UpdatedDate = DateTime.Now;
            
            

            _context.Update(pageComponentCategory);
            _context.SaveChanges();

            return dto;


        }
    }
}
