using Data.Domain.Panel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain
{
    /// <summary>
    /// Ana sayfada kullanılabilecek resim,desc ve title içeren componentler için.
    /// </summary>
    [Table("Components")]
    public class Component:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Href { get; set; }

        public int ComponentCategoryId { get; set; }
        public ComponentCategory ComponentCategory { get; set; }

    }
}
