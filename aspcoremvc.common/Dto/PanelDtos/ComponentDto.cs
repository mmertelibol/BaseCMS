using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class ComponentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Href { get; set; }
        public int CategoryId { get; set; }

        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
