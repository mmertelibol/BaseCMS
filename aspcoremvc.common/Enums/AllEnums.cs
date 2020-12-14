using System.ComponentModel;

namespace Common.Enums
{
    public enum LoginTypes
    {
        Standart,
        Ldap,
    }
    public enum UserTypes
    {
        Registrar,
        Employee,
    }

    public enum MaskType
    {
        Telephone = 1,
        Text = 2,
        IdentityNumber = 3
    }
}
