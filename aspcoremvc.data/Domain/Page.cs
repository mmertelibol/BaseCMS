using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Pages", Schema = "dbo")]
    public class Page : EntityBase
    {
        [NotMapped]
        public string Url { get { return string.IsNullOrEmpty(Controller) && string.IsNullOrEmpty(Action) ? "" : string.Format("/{0}/{1}", Controller, Action); } }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string TitleCode { get; set; }

        public int DisplayOrder { get; set; }

        public string MenuIcon { get; set; }

        public int PageLevel { get; set; }

        public int? ParentId { get; set; }

        public bool HideInMenu { get; set; }

        public string Href { get; set; }
    }
}