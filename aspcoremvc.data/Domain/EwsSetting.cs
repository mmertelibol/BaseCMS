using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("EwsSettings", Schema = "dbo")]
    public class EwsSetting : EntityBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
    }
}