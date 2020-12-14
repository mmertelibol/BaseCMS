using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Dto;
using Common.Dto.DataTablesGrid;

namespace Business.Services.Interfaces
{
    /// <summary>
    /// Jenerik veritabanı işlemlerinde kullanılır. 
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// En son çalıştırılan servis sonucunu döner.
        /// </summary>
        /// <returns></returns>
        ServiceResult GetResult();

        /// <summary>
        /// Gönderilen entity üzerindeki ID'li veriyi siler.
        /// </summary>
        /// <typeparam name="TEntity">Domain</typeparam>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        Task GenericRemoveItem<TEntity>(int id) 
            where TEntity : class;

        /// <summary>
        /// Gönderilen entity üzerindeki veriyi döner.
        /// </summary>
        /// <typeparam name="TEntity">Domain</typeparam>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<TEntity> GenericLoadItem<TEntity>(int id) 
            where TEntity : class;

        /// <summary>
        /// Gönderilen entity üzerindeki veriyi, gönderilen dto tipinde döner. 
        /// </summary>
        /// <typeparam name="TEntitySource">Domain</typeparam>
        /// <typeparam name="TEntityTarget">Dto</typeparam>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<TEntityTarget> GenericLoadItem<TEntitySource, TEntityTarget>(int id) 
            where TEntitySource : class 
            where TEntityTarget : class;

        /// <summary>
        /// Gönderilen entity üzerine, gönderilen datayı kayıt eder.
        /// </summary>
        /// <typeparam name="TEntitySource">Domain</typeparam>
        /// <typeparam name="TEntityTarget">Dto</typeparam>
        /// <param name="dto"></param>
        /// <param name="archiveWithoutUpdate"></param>
        /// <returns></returns>
        Task GenericSaveItem<TEntitySource, TEntityTarget>(TEntitySource dto, bool archiveWithoutUpdate = false) 
            where TEntitySource : IKeyedObject 
            where TEntityTarget : class;

        /// <summary>
        /// Gönderilen entity üzerindeki dataları gride yüklenecek formatta döner.
        /// </summary>
        /// <typeparam name="TEntitySource">Domain</typeparam>
        /// <typeparam name="TEntityTarget">Dto</typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DataTableAjaxResponse> GenericGrid<TEntitySource, TEntityTarget>(DataTableAjaxPostModel request)
            where TEntitySource : class 
            where TEntityTarget : class;

        /// <summary>
        /// Gönderilen fonksiyon üzerindeki dataları gride yüklenecek formatta döner.
        /// </summary>
        /// <typeparam name="TEntitySource"></typeparam>
        /// <typeparam name="TEntityTarget"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<TEntityTarget> GenericLoadItemFunc<TEntitySource, TEntityTarget>(Expression<Func<TEntitySource, bool>> func) 
            where TEntitySource : class 
            where TEntityTarget : class;

        /// <summary>
        /// Gönderilen entity üzerindeki dataları liste halinde döner.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<List<LookupDto>> GenericListItems<TEntity>() 
            where TEntity : class;

        /// <summary>
        /// Prosedür çalıştırmak için kullanılır.
        /// </summary>
        /// <param name="query">Sorgu</param>
        /// <param name="conn">Sql Bağlantı Cümlesi (ConnectionString)</param>
        /// <returns></returns>
        Task<int> ExecuteSp(string query, string conn);
    }
}