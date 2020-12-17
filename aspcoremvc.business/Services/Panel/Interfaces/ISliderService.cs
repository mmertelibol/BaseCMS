using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface ISliderService
    {
        List<SliderDto> GetAllSliders();
        SliderDto GetSliderById(int sliderId);

        SliderDto DeleteSlider(int sliderId);

        SliderDto AddSlider(SliderDto sliderDto);

        SliderDto UpdateSlider(SliderDto sliderDto);

    }
}
