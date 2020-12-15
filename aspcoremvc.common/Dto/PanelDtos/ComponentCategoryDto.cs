using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class ComponentCategoryDto
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
