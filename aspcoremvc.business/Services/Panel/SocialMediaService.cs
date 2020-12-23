using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain.Panel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class SocialMediaService:ISocialMediaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SocialMediaService(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public SocialMediaDto AddSocialMedia(SocialMediaDto socialMediaDto)
        {
            socialMediaDto.AddedDate = DateTime.Now;
            if (socialMediaDto.File != null)
            {
                var uniqueName = Guid.NewGuid() + Path.GetExtension(socialMediaDto.File.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + uniqueName);
                var stream = new FileStream(path, FileMode.Create);
                socialMediaDto.File.CopyTo(stream);

                socialMediaDto.Icon = uniqueName;
            }

                var socialMedia = _mapper.Map<SocialMedia>(socialMediaDto);
            var added = _context.SocialMedia.Add(socialMedia);
            var dtoModel = _mapper.Map<SocialMediaDto>(socialMedia);

            _context.SaveChanges();

            return dtoModel;
            

        }

        public SocialMediaDto DeleteSocialMedia(int id)
        {
            var socialMedia = _context.SocialMedia.Find(id);
            var deleted = _context.SocialMedia.Remove(socialMedia);
            var dtoModel = _mapper.Map<SocialMediaDto>(socialMedia);

            _context.SaveChanges();

            return dtoModel;
        }

        public List<SocialMediaDto> GetAllList()
        {
            var socialMediaList = _context.SocialMedia.Where(x => x.IsDeleted == false).ToList();
            var dtoModel = _mapper.Map<List<SocialMediaDto>>(socialMediaList);

            return dtoModel;
        }

        public SocialMediaDto GetSocialMediaById(int id)
        {
            var socialMedia = _context.SocialMedia.Find(id);
            var dtoModel = _mapper.Map<SocialMediaDto>(socialMedia);

            return dtoModel;
        }

        public SocialMediaDto UpdateSocialMedia(SocialMediaDto socialMediaDto)
        {
            var socialMedia = _context.SocialMedia.Find(socialMediaDto.Id);
            if (socialMediaDto.File != null)
            {
                var uniqueName = Guid.NewGuid() + Path.GetExtension(socialMediaDto.File.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + uniqueName);
                var stream = new FileStream(path, FileMode.Create);
                socialMediaDto.File.CopyTo(stream);

                socialMediaDto.Icon = uniqueName;
            }

            socialMedia.Id = socialMediaDto.Id;
            socialMedia.Href = socialMediaDto.Href;
            socialMedia.Icon = socialMediaDto.Icon;
            socialMedia.IsVisible = socialMediaDto.IsVisible;
            socialMedia.Name = socialMediaDto.Name;
            socialMedia.UpdatedDate = DateTime.Now;

            _context.SocialMedia.Update(socialMedia);
            _context.SaveChanges();

            return socialMediaDto;
          
        }
    }
}
