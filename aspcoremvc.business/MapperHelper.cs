using AutoMapper;
using Common.Dto;
using Common.Dto.PanelDto;
using Common.Dto.PanelDtos;
using Common.Resources;
using Data.Domain;
using Data.Domain.Panel;
using Domain.User;
using System;

namespace Business
{
    public static class MapperHelper
    {
        public static void InitMaps(IServiceProvider serviceProvider)
        {
            var localizer = (LocService)serviceProvider.GetService(typeof(LocService));
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Page, PageDto>();
                cfg.CreateMap<ActionLogDto, ActionLog>();

                // DOMAIN TO DTO
                //cfg.CreateMap<Constant, ConstantDto>().ReverseMap().MaxDepth(1);              
                cfg.CreateMap<User, UserDto>().ReverseMap().MaxDepth(1);                         
                cfg.CreateMap<Language, LanguageDto>().ReverseMap().MaxDepth(1);

                //panel dto to domain
                cfg.CreateMap<NewsDto, News>().ReverseMap();
                cfg.CreateMap<NewsCategoryDto, NewsCategory>().ReverseMap();
                cfg.CreateMap<AddressDto, Address>().ReverseMap();
                cfg.CreateMap<PageComponentCategoryDto, PageComponentCategory>().ReverseMap();
                cfg.CreateMap<PageComponentDto, PageComponent>().ReverseMap();
                cfg.CreateMap<SliderDto, Slider>().ReverseMap();
                cfg.CreateMap<SettingDto, Setting>().ReverseMap();
                cfg.CreateMap<StaticPageDto, StaticPage>().ReverseMap();
                cfg.CreateMap<SocialMediaDto, SocialMedia>().ReverseMap();

                
                
                
                // DTO TO DTO               
                cfg.CreateMap<UserDto, LookupDto>().ForMember(src => src.Text, desc => desc.MapFrom(trgt => $"{trgt.FirstName} {trgt.LastName}"));
  
               
            });



         
        }
    }
}