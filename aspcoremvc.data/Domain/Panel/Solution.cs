using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain
{
    [Table("Solutions")]
    public class Solution:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Href { get; set; }
        //public string  PageContent { get; set; }

      
    }
}
