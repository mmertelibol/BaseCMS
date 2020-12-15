using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class SliderDto
    {
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int OrderNumber { get; set; }

        public string PageUrl { get; set; }

        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
