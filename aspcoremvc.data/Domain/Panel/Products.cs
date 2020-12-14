using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Panel
{
    public class Products:EntityBase
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string PageUrl { get; set; }
    }
}
