using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.User
{ 
    [Table("UserClaims",Schema = "User")]
    public class UserClaim : IdentityUserClaim<int> { }

    [Table("RoleClaims", Schema = "User")]
    public class RoleClaim : IdentityRoleClaim<int> { }

    [Table("UserTokens", Schema = "User")]
    public class UserToken : IdentityUserToken<int> { }

   
}