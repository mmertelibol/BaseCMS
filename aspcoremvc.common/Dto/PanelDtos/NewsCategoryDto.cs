using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class NewsCategoryDto
    {
        
        public string Name { get; set; }

        public int Id { get; set; }


        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public List<NewsDto> NewsDto { get; set; }


        public int NewsCategoryId { get; set; }


    }
}
