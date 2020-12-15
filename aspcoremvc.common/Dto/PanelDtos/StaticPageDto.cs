using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDtos
{
    public class StaticPageDto
    {
        public string Name { get; set; }
        public string HTMLContent { get; set; }

        public string Title { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string RoutingUrl { get; set; }

        public int Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
