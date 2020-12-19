using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class NewsDto
    {
        public string HTMLContent { get; set; }

        public string Title { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string RoutingUrl { get; set; }

        public int? ViewCount { get; set; }

        public string Author { get; set; }

        public int NewsCategoryId { get; set; }

       

        public NewsCategoryDto NewsCategory { get; set; }

        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
