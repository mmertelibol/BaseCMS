using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("PageComponentCategories")]
   public class PageComponentCategory:EntityBase
    {
        public string Name { get; set; }

        public  ICollection<PageComponent> PageComponents { get; set; }

    }
}
