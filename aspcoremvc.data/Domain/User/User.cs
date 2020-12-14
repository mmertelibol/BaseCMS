using Data.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.User
{
    [Table("Users", Schema = "User")]
    public class User : IdentityUser<int>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }      
        public DateTime LastLoginDate { get; set; }
        public string FullName => $"{FirstName} {LastName}";  
        public string NormalizedFullName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? AddedById { get; set; }
        [ForeignKey("AddedById")]
        public User AddedBy { get; set; }
        public int? UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }
        public int? UserTypeId { get; set; }
        public Constant UserType { get; set; }//Registrar,Employee
        public bool IsEmployeeHost { get; set; } 
    }
}
