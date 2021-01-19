using Common.Dto;
using Common.Dto.DataTablesGrid;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    /// <summary>
    /// Kullanıcı işlemleri için kullanılır.
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// Kullanıcı getirir.
        /// </summary>
        /// <param name="id">Kullanıcı Id</param>
        /// <returns></returns>
        UserDto GetUser(int id);
        UserDto CheckUserPhone(string phone, int locationId);


        Task<bool> Login(UserDto userdto);
        Task<bool> SignOut();


    }
}