using AutoMapper;
using Common.Dto;
using Common.Resources;
using Data.Domain;
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
                cfg.CreateMap<Constant, ConstantDto>().ReverseMap().MaxDepth(1);              
                cfg.CreateMap<User, UserDto>().ReverseMap().MaxDepth(1);                         
                cfg.CreateMap<Language, LanguageDto>().ReverseMap().MaxDepth(1);                
                
                // DTO TO DTO               
                cfg.CreateMap<UserDto, LookupDto>().ForMember(src => src.Text, desc => desc.MapFrom(trgt => $"{trgt.FirstName} {trgt.LastName}"));
  
            });
        }
    }
}