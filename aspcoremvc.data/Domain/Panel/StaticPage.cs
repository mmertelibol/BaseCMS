using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    /// <summary>
    /// statik sayfalar için kullanılır(hakkımızda vs)
    /// </summary>
    [Table("StaticPages")]
    public class StaticPage:EntityBase
    {
        public string Name { get; set; }
        public string HTMLContent { get; set; }

        public string Title { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string RoutingUrl { get; set; }



    }
}
