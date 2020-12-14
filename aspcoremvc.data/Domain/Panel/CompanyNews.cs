using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Panel
{
    
   public class CompanyNews:EntityBase
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PageUrl { get; set; }

        public string ContentText { get; set; }


    }
}
