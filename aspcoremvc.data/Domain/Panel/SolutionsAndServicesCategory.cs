using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
   public class SolutionsAndServicesCategory:EntityBase
    {
        public string Name { get; set; }

        public ICollection<SolutionsAndServices> SolutionsAndServices { get; set; }
    }
}
