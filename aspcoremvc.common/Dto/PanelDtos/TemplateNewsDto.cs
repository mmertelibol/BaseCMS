using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDtos
{
    public class TemplateNewsDto
    {
        public List<NewsDto> News { get; set; }
        public NewsCategoryDto NewsCategoryDto { get; set; }

    }
}
