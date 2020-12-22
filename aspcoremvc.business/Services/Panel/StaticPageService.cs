using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDtos;
using Data;
using Data.Domain.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class StaticPageService:IStaticPageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StaticPageService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StaticPageDto AddStaticPage(StaticPageDto staticPageDto)
        {
            staticPageDto.AddedDate = DateTime.Now;
            var staticPage = _mapper.Map<StaticPage>(staticPageDto);
            var added = _context.StaticPage.Add(staticPage);
            var dtoModel = _mapper.Map<StaticPageDto>(staticPage);

            _context.SaveChanges();
            return dtoModel;
        }

        public StaticPageDto DeleteStaticPage(int id)
        {
            var staticPage = _context.StaticPage.Find(id);
            var deleted = _context.StaticPage.Remove(staticPage);
            var dtoModel = _mapper.Map<StaticPageDto>(staticPage);

            _context.SaveChanges();
            return dtoModel;
        }

        public List<StaticPageDto> GetAllStaticPage()
        {
            var staticPageList = _context.StaticPage.Where(x => x.IsDeleted == false).ToList();
            var dtoModel = _mapper.Map<List<StaticPageDto>>(staticPageList);

            return dtoModel;
        }

        public StaticPageDto GetStaticPageById(int id)
        {
            var staticPage = _context.StaticPage.Find(id);
            var dtoModel = _mapper.Map<StaticPageDto>(staticPage);

            return dtoModel;

        }

        public StaticPageDto UpdateStaticPage(StaticPageDto staticPageDto)
        {
            var staticPage = _context.StaticPage.Find(staticPageDto.Id);

            staticPage.Id = staticPageDto.Id;
            staticPage.MetaDescription = staticPageDto.MetaDescription;
            staticPage.MetaKeywords = staticPageDto.MetaKeywords;
            staticPageDto.Name = staticPageDto.Name;
            staticPage.RoutingUrl = staticPageDto.RoutingUrl;
            staticPage.UpdatedDate = DateTime.Now;

            _context.StaticPage.Update(staticPage);

            _context.SaveChanges();

            return staticPageDto;
        }
    }
}
