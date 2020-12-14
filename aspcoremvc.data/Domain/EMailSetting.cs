using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.User;

namespace Data.Domain
{
    [Table(name: "EMailSettings", Schema = "dbo")]
    public class EMailSetting : EntityBase
    {
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
