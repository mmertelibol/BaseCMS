using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
    public class SettingDto
    {
        public string CompanyName { get; set; }
        public string Slogan { get; set; }

        public string FavIconUrl { get; set; } //buna bak
        public string LogoUrl { get; set; }

        public int Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public IFormFile file { get; set; }
    }
}
