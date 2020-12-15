using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("Directors")]
    public class Director:EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string PageContent { get; set; }
        public string Description { get; set; }



    }
}
