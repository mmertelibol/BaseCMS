using AutoMapper;
using Common.Dto;
using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
using Data.Domain;
using Data.Domain.Panel;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.AutoMapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<NewsDto, News>().ReverseMap();
            CreateMap<NewsCategoryDto, NewsCategory>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<PageComponentCategoryDto, PageComponentCategory>().ReverseMap();
            CreateMap<PageComponentDto, PageComponent>().ReverseMap();
            CreateMap<SliderDto, Slider>().ReverseMap();
            CreateMap<SettingDto, Setting>().ReverseMap();
            CreateMap<StaticPageDto, StaticPage>().ReverseMap();
            CreateMap<SocialMediaDto, SocialMedia>().ReverseMap();
          


        }



    }
}
