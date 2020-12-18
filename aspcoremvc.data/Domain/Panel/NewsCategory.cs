using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("NewsCategories")]
   public class NewsCategory:EntityBase
    {
        public string Name { get; set; }

        public  ICollection<News> News { get; set; }
    }
}
