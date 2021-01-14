using Data.Domain;
using Data.Domain.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ViewModel
    {
        public IEnumerable<PageComponent> PageComponents { get; set; }
        public IEnumerable<News> News { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable <SocialMedia> SocialMedias { get; set; }
        public IEnumerable <StaticPage> StaticPages { get; set; }

        

    }
}
