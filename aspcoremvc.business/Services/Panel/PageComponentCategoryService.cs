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
            var addedPageComponentCategory = _context.PageComponentCategory.Add(pageComponentCategory);

            var dtoModel = _mapper.Map<PageComponentCategoryDto>(addedPageComponentCategory);

            _context.SaveChanges();
            return dtoModel;


        }

        public PageComponentCategoryDto DeletePageComponentCategory(int id)
        {
            var pageComponentCategory = _context.PageComponentCategory.Find(id);

            var deletedSetting = _context.PageComponentCategory.Remove(pageComponentCategory);
            var dtoModel = _mapper.Map<PageComponentCategoryDto>(deletedSetting);

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
            var pageComponentCategory = _context.Setting.Find(id);

            var dtoModel = _mapper.Map<PageComponentCategoryDto>(pageComponentCategory);

            return dtoModel;
        }

        public PageComponentCategoryDto UpdatePageComponentCategory(PageComponentCategoryDto dto)
        {
            var pageComponentCategory = _context.PageComponentCategory.Find(dto.Id);
            pageComponentCategory.Id = dto.Id;
            pageComponentCategory.Name = dto.Name;
            pageComponentCategory.UpdatedDate = dto.UpdatedDate;
            

            _context.Update(pageComponentCategory);
            _context.SaveChanges();

            return dto;


        }
    }
}
