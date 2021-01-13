using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDtos
{
   public class CombineDto
    {
        public IEnumerable<PageComponentDto> PageComponent { get; set; }
        public IEnumerable <NewsDto> News { get; set; }

    }
}
