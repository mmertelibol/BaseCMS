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
    public class SettingService:ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SettingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SettingDto AddSetting(SettingDto settingDto)
        {
            settingDto.AddedDate = DateTime.Now;
            var setting = _mapper.Map<Setting>(settingDto);
            var addedSetting = _context.Setting.Add(setting);

            var dtoModel = _mapper.Map<SettingDto>(addedSetting);

            _context.SaveChanges();
            return dtoModel;

            
        }

        public SettingDto DeleteSetting(int SettingId)
        {
            var setting = _context.Setting.Find(SettingId);

            var deletedSetting = _context.Setting.Remove(setting);
            var dtoModel = _mapper.Map<SettingDto>(deletedSetting);

            _context.SaveChanges();

            return dtoModel;
        }

        public List<SettingDto> GetAllSettings()
        {
            var settingList = _context.Setting.Where(x => x.IsDeleted == false).ToList();
            var dtoModel = _mapper.Map<List<SettingDto>>(settingList);

            return dtoModel;
        }

        public SettingDto GetSettingById(int settingId)
        {
            var setting = _context.Setting.Find(settingId);

            var dtoModel = _mapper.Map<SettingDto>(setting);

            return dtoModel;
        }

        public SettingDto UpdateSetting(SettingDto settingDto)
        {
            var setting = _context.Setting.Find(settingDto.Id);
            setting.Id = settingDto.Id;
            setting.CompanyName = settingDto.CompanyName;
            setting.FavIconUrl = settingDto.FavIconUrl;
            setting.UpdatedDate = DateTime.Now;
            setting.Slogan = settingDto.Slogan;

            _context.Update(setting);
            _context.SaveChanges();

            return settingDto;

            
        }
    }
}