using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class SliderService:ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SliderService(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public SliderDto AddSlider(SliderDto sliderDto)
        {
            sliderDto.AddedDate = DateTime.Now;
            var slider = _mapper.Map<Slider>(sliderDto);
            var addedSlider = _context.Slider.Add(slider);
            var sliderDtoModel = _mapper.Map<SliderDto>(addedSlider);

            _context.SaveChanges();
            return sliderDtoModel;

        }

        public SliderDto DeleteSlider(int sliderId)
        {
            var slider = _context.Slider.Find(sliderId);
            var deleted = _context.Slider.Remove(slider);
            var sliderDtoModel = _mapper.Map<SliderDto>(deleted);

            _context.SaveChanges();
            return sliderDtoModel;

        }

        public SliderDto GetSliderById(int sliderId)
        {
            var slider = _context.Slider.Find(sliderId);

            var sliderDtoModel = _mapper.Map<SliderDto>(slider);

            return sliderDtoModel;
        }

        public List<SliderDto> GetAllSliders()
        {
            var sliderList = _context.Slider.Where(x => x.IsDeleted == false).ToList();
            var sliderDtoModel = _mapper.Map<List<SliderDto>>(sliderList);

            return sliderDtoModel;
        }

        public SliderDto UpdateSlider(SliderDto sliderDto)
        {
            var slider = _context.Slider.Find(sliderDto.Id);
            slider.Description = sliderDto.Description;
            slider.Order = sliderDto.Order;
            slider.Id = sliderDto.Id;
            slider.ImageUrl = sliderDto.ImageUrl;
            slider.UpdatedDate = DateTime.Now;
            slider.Href = sliderDto.Href;

            _context.Slider.Update(slider);
            _context.SaveChanges();

            return sliderDto;
        }
    }
}
