using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("ComponentCategories")]
   public class ComponentCategory:EntityBase
    {
        public string Name { get; set; }

        public ICollection<Component> Components { get; set; }

    }
}
