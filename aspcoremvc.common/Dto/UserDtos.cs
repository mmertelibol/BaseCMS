using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Dto.DataTablesGrid;

namespace Common.Dto
{
    public class UserDto
    {


       

        public int Id { get; set; }

        [Required(ErrorMessage ="Mail alanı zorunlu!")]
        public string Email { get; set; }
        public string  UserName { get; set; }
        [Required(ErrorMessage = "Password alanı zorunlu!")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NormalizedFullName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastLoginDate { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        //ROL EKLEME İÇİN
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Exists { get; set; }
    }

    public class UserRolePostDto : DataTableAjaxPostModel
    {
        public string Type1 { get; set; }
    }

    public class UserRoleDto : GridRowBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RolesToString { get; set; }
    }



    public class RoleDto
    {
        public int? RoleId { get; set; }

        public string RoleName { get; set; }

      
    }

    public class UpdateUserRolesModel
    {
        public UpdateUserRolesModel()
        {
            this.Roles = new List<string>();
        }
        public int UserId { get; set; }

        public List<string> Roles { get; set; }

        public string NewRoleName { get; set; }
    }

}