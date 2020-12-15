using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class DirectorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string PageContent { get; set; }
        public string Description { get; set; }

        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
