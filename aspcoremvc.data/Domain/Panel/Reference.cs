using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain
{
    [Table("References")]
    public class Reference:EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PageContent { get; set; }

        
        public string LogoUrl { get; set; }

        
    }
}
