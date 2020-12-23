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
        public bool IsVisible { get; set; } //ui tarafında görünsün mü görünmesin mi
        public string Icon { get; set; }
        public string Href { get; set; }
    }
}
