using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto
{
    public class EmailContactFormDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }

    public class EmailLongTermRentFormDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BrandName { get; set; }
        public string RentDurationName { get; set; }
        public string CustomerTypeName { get; set; }
        public string LegalPersonName { get; set; }
        public string ETKAccept { get; set; }

        /// <summary>
        /// Marka
        /// </summary>
        public int Brand { get; set; }


    }

}
