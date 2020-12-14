using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Panel
{
   public class GeneralOptions:EntityBase //iletişime ayrıca gerek var mı sor//settings olarak degiş
    {
        public string SiteTitle { get; set; }
        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string LogoUrl { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public string LinkedIn { get; set; }

        public string GoogleMaps { get; set; }
    }
}
