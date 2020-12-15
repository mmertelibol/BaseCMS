using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Panel
{
   public class NewsCategory:EntityBase
    {
        public string Name { get; set; }

        public ICollection<News> News { get; set; }
    }
}
