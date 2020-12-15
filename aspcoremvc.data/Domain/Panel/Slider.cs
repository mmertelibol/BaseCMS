using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain
{
    [Table("Sliders")]
    public class Slider:EntityBase
    {
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int OrderNumber { get; set; }

        public string Href { get; set; }

        
    }
}
