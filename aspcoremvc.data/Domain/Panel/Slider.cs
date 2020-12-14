using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
   public class Slider:EntityBase
    {
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int OrderNumber { get; set; }

        public string PageUrl { get; set; }
    }
}
