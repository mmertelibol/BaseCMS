using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto.PanelDto
{

    public class SocialMediaDto
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public string Icon { get; set; }
        public string Href { get; set; }

        //[Required, FileExtensions(Extensions = ".png", ErrorMessage = "Incorrect file format")]
        public IFormFile File { get; set; }
        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
