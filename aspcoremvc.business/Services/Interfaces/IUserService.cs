using Common.Dto;
using Common.Dto.DataTablesGrid;
using Common.Dto.PanelDtos;
using System.Collections.Generic;
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


        Task<bool> Login(UserDto userDto);
        Task<bool> SignOut();
        List<UserDto> GetAllUser();
        Task<List<UserDto>> AssignRole(int id);
        Task<List<UserDto>> AssignRolePost(List<UserDto> assignRoles,int id);

       


    }
}