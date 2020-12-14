using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.User
{
    [Table("UserLogins", Schema = "User")]
    public class UserLogin : IdentityUserLogin<int> { }
}