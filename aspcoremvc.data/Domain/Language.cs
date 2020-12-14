using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Languages", Schema = "dbo")]
    public class Language : EntityBase
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }
    }
}