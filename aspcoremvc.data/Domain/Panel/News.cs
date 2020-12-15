using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("News")]
    public class News : EntityBase
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PageUrl { get; set; }

        


    }
}
