using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto
{
    public class EmailSettingDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsActive { get; set; }
    }
}
