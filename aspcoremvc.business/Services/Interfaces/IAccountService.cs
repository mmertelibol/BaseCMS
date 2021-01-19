using System.Threading.Tasks;
using System.Collections.Generic;
using Data;
using Common.Dto;
using Common.Dto.DataTablesGrid;
using Domain.User;
using Data.Domain;

namespace Business.Services.Interfaces
{
    /// <summary>
    /// Hesap işlemleri için kullanılır.
    /// </summary>
    public interface IAccountService : IBaseService
    {

        /// <summary>
        /// Kullanıcı oturum kapatma işleminden sonra yapılacak servis işlemleri için kullanılır.
        /// Cache vs. temizlenir.
        /// </summary>
        /// <returns></returns>
        bool LogoutUser();

        /// <summary>
        /// Kullanıcı parametresine göre ona ait rol listesini dönen methoddur.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Role> GetUserRoles(int userId);

        /// <summary>
        /// Kullanıcı ve Rollerini DataTable üzerinde görüntülemek üzere kullanılan methoddur.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<DataTableAjaxResponse> GetUserAndRoles(UserRolePostDto request);

        /// <summary>
        /// Sistemdeki tüm rol listesini dönmek için kullanılan methoddur.
        /// </summary>
        /// <returns></returns>
        List<RoleDto> GetAllRoles();

        /// <summary>
        /// Tüm menü listesini dönen methoddur.
        /// </summary>
        /// <returns></returns>
        List<PageDto> GetAllMenu();

        /// <summary>
        /// Log kayıt eden methoddur.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int AddActionLog(ActionLogDto dto);

        /// <summary>
        /// Log güncelleyen methoddur.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hasException"></param>
        void UpdateActionLog(int id, bool hasException);

        /// <summary>
        /// ID bilgisine göre sayfa detaylarını getiren methoddur.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Page> FindPageAsync(int Id);

        /// <summary>
        /// Telefon numarası kontrolü yapan methoddur.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> CheckPhoneNumber(string phone);

        /// <summary>
        /// Email kontrolü yapan methoddur.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CheckUserEmail(string email);

        /// <summary>
        /// Kullanıcı bilgilerini ve rollerini getiren methoddur.
        /// </summary>
        /// <param name="userId">Kullanıcı</param>
        /// <returns></returns>
        UserRoleDto GetUserRole(int userId);

        /// <summary>
        /// Parola sıfırlamak için kullanılan methoddur.
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <param name="email">Email</param>
        /// <returns></returns>
        Task<JsonResultDto> ResetPassword(string username, string email);

        /// <summary>
        /// Sıfırlanan parolaya yeni parola kayıt etmek için kullanılan methoddur.
        /// </summary>
        /// <param name="token">Parola Sıfırlama Token'i</param>
        /// <param name="username">Kullanıcı Adı</param>
        /// <param name="email">Email</param>
        /// <param name="newPassword">Yeni Şifre</param>
        /// <returns></returns>
        Task<JsonResultDto> SetPassword(string token, string username, string email, string newPassword);

        /// <summary>
        /// Kullanıcı Bilgilerini dönen methoddur.
        /// </summary>
        /// <param name="id">Kullanıcı ID</param>
        /// <returns></returns>
        Task<UserDto> GetUser(int id);

        Task<RoleDto> AddRole(RoleDto roleDto);
    }
}