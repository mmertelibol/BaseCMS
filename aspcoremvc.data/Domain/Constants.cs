using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Constants", Schema = "dbo")]
    public class Constant
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Code { get; set; }
        [StringLength(500)]
        public string DescriptionCode { get; set; }
        [StringLength(100)]
        public string GroupCode { get; set; }
        [StringLength(100)]
        public string ParentGroupCode { get; set; }

        public DateTime? AddedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int DisplayOrder { get; set; }
    }
}