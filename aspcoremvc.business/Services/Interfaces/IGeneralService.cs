using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto;

namespace Business.Services.Interfaces
{
    /// <summary>
    /// Genel işlemler için kullanılır.
    /// </summary>
    public interface IGeneralService : IBaseService
    {
        /// <summary>
        /// Dil listesini dönen methoddur.
        /// </summary>
        /// <returns></returns>
        Task<List<LanguageDto>> ListLanguages();
    }
}