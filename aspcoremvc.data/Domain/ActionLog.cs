using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("ActionLogs", Schema = "dbo")]
    public class ActionLog
    {
        public int Id { get; set; }

        public DateTime RequestStart { get; set; }

        public DateTime RequestEnd { get; set; }

        [StringLength(50)]
        public string Controller { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        public string Parameters { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        public string Referer { get; set; }

        [StringLength(50)]
        public string ClientIp { get; set; }

        public decimal ElapsedTimeInSeconds { get; set; }

        public bool HasException { get; set; }
    }
}