using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDto
{
   public class AdressDto
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

        

        public int Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
