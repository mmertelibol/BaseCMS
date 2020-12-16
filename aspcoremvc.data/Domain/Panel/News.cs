using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    /// <summary>
    /// blog içerigi için kullanılır.
    /// </summary>
    [Table("News")]
    public class News : EntityBase
    {
      
        public string HTMLContent { get; set; }

        public string Title { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string RoutingUrl { get; set; }

        public int ViewCount { get; set; }

        public string Author { get; set; }

        public int NewsCategoryId { get; set; }
        public NewsCategory NewsCategory { get; set; }




    }
}
