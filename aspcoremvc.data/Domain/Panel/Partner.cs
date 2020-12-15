using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("Partners")]
    public class Partner:EntityBase
    {
        
        public string LogoUrl { get; set; }
        
       

       


    }
}
