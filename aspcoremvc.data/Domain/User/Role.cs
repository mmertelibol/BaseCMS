using System;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.User
{
    [Table("Roles", Schema = "User")]
    public class Role : IdentityRole<int>
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? AddedById { get; set; }
        public User AddedBy { get; set; }
        public int? UpdatedById { get; set; }
        public User UpdatedBy { get; set; }
        public int StartPageId { get; set; }
        public Page StartPage { get; set; }
    }
}