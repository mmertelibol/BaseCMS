using Domain.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.User
{
    [Table("UserRoles", Schema = "User")]
    public class UserRole : IdentityUserRole<int>
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? AddedById { get; set; }
        public User AddedBy { get; set; }
        public int? UpdatedById { get; set; }
        public User UpdatedBy { get; set; }
    }
}
