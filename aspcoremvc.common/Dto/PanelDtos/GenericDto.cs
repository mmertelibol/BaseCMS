using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDtos
{
    public class GenericDto
    {
        public IEnumerable<PageComponentDto> PageComponentDto { get; set; }
        public IEnumerable<NewsDto> NewsDto { get; set; }
        public IEnumerable<AddressDto> AddressDto { get; set; }
        public IEnumerable<SliderDto> SliderDto { get; set; }
        public IEnumerable<SocialMediaDto> SocialMediaDto { get; set; }
        public IEnumerable<StaticPageDto> StaticPageDto { get; set; }
    }
}
