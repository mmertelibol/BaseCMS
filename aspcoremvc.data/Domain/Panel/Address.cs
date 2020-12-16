using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Domain.Panel
{
    [Table("Addresses")]
    public class Address:EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string FullAdress { get; set; }
        /// <summary>
        /// boylam
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// enlem
        /// </summary>
        public string Latitude { get; set; }
    }
}
