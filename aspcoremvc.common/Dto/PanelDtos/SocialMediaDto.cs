using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{

    public class SocialMediaDto
    {
        public string Name { get; set; }
        public string IsVisible { get; set; }
        public string Icon { get; set; }
        public string Href { get; set; }

        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
