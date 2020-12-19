using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain;
using Data.Domain.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class PageComponentService:IPageComponentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PageComponentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public PageComponentDto AddPageComponent(PageComponentDto pageComponentDto)
        {
            pageComponentDto.AddedDate = DateTime.Now;
            var pageComponent = _mapper.Map<PageComponent>(pageComponentDto);
            var added = _context.PageComponent.Add(pageComponent);
            var dtoModel = _mapper.Map<PageComponentDto>(pageComponent);

            _context.SaveChanges();
            return dtoModel;

        }

        public PageComponentDto DeletePageComponent(int id)
        {
            var pageComponent = _context.PageComponent.Find(id);
            var deletedPageComponent = _context.PageComponent.Remove(pageComponent);
            var dtoModel = _mapper.Map<PageComponentDto>(deletedPageComponent);

            _context.SaveChanges();
            return dtoModel;

        }

        public PageComponentDto GetPageComponentById(int id)
        {
            var pageComponent = _context.PageComponent.Find(id);

            var dtoModel = _mapper.Map<PageComponentDto>(pageComponent);

            return dtoModel;
        }

        public List<PageComponentDto> GetAllPageComponents()
        {
            var componentsList = _context.PageComponent.Where(x => x.IsDeleted == false).ToList();
            var dtoModel = _mapper.Map<List<PageComponentDto>>(componentsList);

            return dtoModel;
        }

        public PageComponentDto UpdatePageComponent(PageComponentDto pageComponentDto)
        {
            var pageComponent = _context.PageComponent.Find(pageComponentDto.Id);

            pageComponent.Id = pageComponentDto.Id;
            pageComponent.Href = pageComponentDto.Href;
            pageComponent.ImageUrl = pageComponent.ImageUrl;
            pageComponent.ComponentCategoryId = pageComponentDto.ComponentCategoryId;
            pageComponent.UpdatedDate = DateTime.Now;
            pageComponent.Description = pageComponentDto.Description;

            _context.PageComponent.Update(pageComponent);
            _context.SaveChanges();

            return pageComponentDto;
        }


    }
}
