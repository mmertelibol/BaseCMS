using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    /// <summary>
    /// site kimliği için kullanılacak
    /// </summary>
    [Table("Settings")]
    public class Setting:EntityBase //iletişime ayrıca gerek var mı sor//settings olarak degiş
    {
        public string CompanyName { get; set; }
        public string Slogan { get; set; }

        public string FavIconUrl { get; set; } //buna bak
        public string LogoUrl { get; set; }

       

     
    }
}
