using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("Contents")]
    public class Content:EntityBase
    {
        public string Name { get; set; }
        public string PageContent { get; set; }

        





    }
}
