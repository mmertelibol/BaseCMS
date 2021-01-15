using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class GenericService : IGenericService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GenericService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenericDto GetAllDto(GenericDto genericDto)
        {
            genericDto.SocialMediaDto = _mapper.Map<List<SocialMediaDto>>(_context.SocialMedia.Where(x => x.IsDeleted == false));
            genericDto.StaticPageDto = _mapper.Map<List<StaticPageDto>>(_context.StaticPage.Where(x => x.IsDeleted == false));
            genericDto.NewsDto = _mapper.Map<List<NewsDto>>(_context.News.Where(x => x.IsDeleted == false));
            genericDto.PageComponentDto = _mapper.Map<List<PageComponentDto>>(_context.PageComponent.Where(x => x.IsDeleted == false));
            genericDto.SliderDto = _mapper.Map<List<SliderDto>>(_context.Slider.Where(x => x.IsDeleted == false));
            genericDto.AddressDto = _mapper.Map<List<AddressDto>>(_context.Adress.Where(x => x.IsDeleted == false));

            return genericDto;
        }
    }
}
