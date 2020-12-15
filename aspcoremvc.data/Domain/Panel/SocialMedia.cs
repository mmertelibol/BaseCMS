using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("SocialMedia")]
   public class SocialMedia:EntityBase
    {
        public string Name { get; set; }
        public string IsVisible { get; set; }
        public string Icon { get; set; }
        public string Href { get; set; }
    }
}
