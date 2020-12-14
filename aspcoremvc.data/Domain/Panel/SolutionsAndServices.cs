using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
   public class SolutionsAndServices:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string PageUrl { get; set; }

        public string ContentText { get; set; }
        public int CategoryId { get; set; }
        public SolutionsAndServicesCategory Category { get; set; }
    }
}
