using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain.Panel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public  SettingDto AddSetting(SettingDto settingDto)
        {
            settingDto.AddedDate = DateTime.Now;
            if (settingDto.file != null)
            {
                var uniqueName = Guid.NewGuid() + Path.GetExtension(settingDto.file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + uniqueName);
                var stream = new FileStream(path,FileMode.Create);
                settingDto.FileLogo.CopyTo(stream);

                settingDto.FavIconUrl = uniqueName;

                
            }
          

            if (settingDto.FileLogo != null)
            {
                var uniquePictureName = Guid.NewGuid() + Path.GetExtension(settingDto.file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + uniquePictureName);
                var stream = new FileStream(path, FileMode.Create);
                settingDto.file.CopyTo(stream);

                settingDto.LogoUrl = uniquePictureName;

            }



            var setting = _mapper.Map<Setting>(settingDto);
            var addedSetting = _context.Setting.Add(setting);

            var dtoModel = _mapper.Map<SettingDto>(setting);

            _context.SaveChanges();
            return dtoModel;

            
        }

        public SettingDto DeleteSetting(int SettingId)
        {
            var setting = _context.Setting.Find(SettingId);

            var deletedSetting = _context.Setting.Remove(setting);
            var dtoModel = _mapper.Map<SettingDto>(setting);

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

            if (settingDto.file != null)
            {
                var uniqueName = Guid.NewGuid() + Path.GetExtension(settingDto.file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + uniqueName);
                var stream = new FileStream(path, FileMode.Create);
                settingDto.file.CopyTo(stream);

                settingDto.FavIconUrl = uniqueName;
                setting.FavIconUrl = settingDto.FavIconUrl;

            }
            else
            {
                setting.FavIconUrl = setting.FavIconUrl;
            }
            if (settingDto.FileLogo != null)
            {
                var uniquePictureName = Guid.NewGuid() + Path.GetExtension(settingDto.FileLogo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + uniquePictureName);
                var stream = new FileStream(path, FileMode.Create);
                settingDto.FileLogo.CopyTo(stream);

                settingDto.LogoUrl = uniquePictureName;
                setting.LogoUrl = settingDto.LogoUrl;

            }

            else
            {
                setting.LogoUrl = setting.LogoUrl;
            }


            
            setting.Id = settingDto.Id;
            setting.CompanyName = settingDto.CompanyName;
            //setting.FavIconUrl = settingDto.FavIconUrl;
            setting.UpdatedDate = DateTime.Now;
            setting.Slogan = settingDto.Slogan;
            //setting.LogoUrl = settingDto.LogoUrl;

            _context.Update(setting);
            _context.SaveChanges();

            return settingDto;

            
        }
    }
}
