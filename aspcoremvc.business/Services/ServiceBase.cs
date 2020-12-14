using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data;
using Common;
using Common.Dto;
using Common.Helpers;
using Common.Resources;
using Microsoft.EntityFrameworkCore;
using Common.Dto.DataTablesGrid;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Services.Interfaces;

namespace Business.Services
{
    public abstract class ServiceBase :IBaseService
    {
        private readonly AppDbContext _context;
        protected readonly LocService Localizer;
        protected ServiceResult ServiceResult;

        protected ServiceBase(AppDbContext parContext, LocService parLocService)
        {
            _context = parContext;
            Localizer = parLocService;
            ServiceResult = new ServiceResult();
        }

        public ServiceResult GetResult() => ServiceResult;

        public void SetResult(ResultCode code, string message = null, object data = null)
        {
            if (message == null)
            {
                if (code == ResultCode.Success)
                    message = Localizer.Get("OperationSucceedText");
                else if (code == ResultCode.Fail)
                    message = Localizer.Get("OperationFailedText");
            }

            ServiceResult = new ServiceResult
            {
                Code = code,
                Message = message,
                Data = data
            };
        }

        public async Task<List<LookupDto>> GenericListItems<TEntity>()
            where TEntity : class
        {
            var entitySet = _context.Set<TEntity>();

            return await entitySet
                .Where("IsDeleted=false")
                .ProjectTo<LookupDto>()
                .ToListAsync();
        }

        public async Task GenericRemoveItem<TEntity>(int id)
            where TEntity : class
        {
            var entitySet = _context.Set<TEntity>();
            var dbItem = entitySet.Find(id);

            entitySet.Remove(dbItem);
            await _context.SaveChangesAsync();
            SetResult(ResultCode.Success);
        }

        public async Task<TEntity> GenericLoadItem<TEntity>(int id)
            where TEntity : class
        {
            return await GenericLoadItem<TEntity, TEntity>(id);
        }

        public async Task<TEntityTarget> GenericLoadItem<TEntitySource, TEntityTarget>(int id)
            where TEntitySource : class
            where TEntityTarget : class
        {
            var entitySet = _context.Set<TEntitySource>();
            var dto = GetNew<TEntityTarget>();

            if (id > 0)
            {
                var dbItem = await entitySet.FindAsync(id);
                dto = Mapper.Map<TEntityTarget>(dbItem);
            }

            return dto;
        }

        public async Task GenericSaveItem<TEntitySource, TEntityTarget>(TEntitySource dto, bool archiveWithoutUpdate = false)
            where TEntitySource : IKeyedObject
            where TEntityTarget : class
        {
            var cacheService = HttpHelper.GetService<CacheBase>();
            var entitySet = _context.Set<TEntityTarget>();
            var dbItem = GetNew<TEntityTarget>();
            var dbItemNew = GetNew<TEntityTarget>();

            if (dto.Id > 0)
                dbItem = await entitySet.FindAsync(dto.Id);

            if (dto.Id > 0 && archiveWithoutUpdate)
            {
                entitySet.Remove(dbItem);
                _context.SaveChanges();

                dto.Id = 0;
                dbItemNew = Mapper.Map(dto, dbItemNew);
                entitySet.Add(dbItemNew);
            }
            else
            {
                dbItem = Mapper.Map(dto, dbItem);

                if (dto.Id <= 0)
                    entitySet.Add(dbItem);
            }

            _context.SaveChanges();
            SetResult(ResultCode.Success, Localizer.Get("OperationSucceedText"), (dbItem as IKeyedObject).Id);
        }

        public async Task<DataTableAjaxResponse> GenericGrid<TEntitySource, TEntityTarget>(DataTableAjaxPostModel request)
            where TEntitySource : class
            where TEntityTarget : class
        {
            var entitySet = _context.Set<TEntitySource>();

            var query = entitySet
                .Where("IsDeleted=false")
                .ProjectTo<TEntityTarget>();

            return await query.ApplyGridBase(request);
        }

        public async Task<TEntityTarget> GenericLoadItemFunc<TEntitySource, TEntityTarget>(Expression<Func<TEntitySource, bool>> func) where TEntitySource : class where TEntityTarget : class
        {
            var entityset = _context.Set<TEntitySource>();
            var dto = GetNew<TEntityTarget>();

            var dbitem = entityset.Where(func).ProjectTo<TEntityTarget>().FirstOrDefault();
            if (dbitem != null)
            {
                dto = Mapper.Map<TEntityTarget>(dbitem);

            }

            return await Task.FromResult(dto);
        }

        public async Task<int> ExecuteSp(string query, string conn)
        {
            var result = DataHelper.ExecuteNewSqlWithConnStr<int>(query, null, true, conn);
            SetResult(ResultCode.Success);

            return await Task.FromResult(0);
        }

        private T GetNew<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }

    public class ServiceResult
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public ResultCode Code { get; set; }
    }

    public enum ResultCode
    {
        Success = 101,
        Information = 203,
        InformationWithDataBind = 204,
        Unauthorized = 401,
        NotFound = 404,
        Fail = 500,
        NullReference = 505,
    }

    public static class ServiceExtensions
    {
        public static async Task<DataTableAjaxResponse> ApplyGridBase<T>(this IQueryable<T> query, DataTableAjaxPostModel request)
        {
            var response = new DataTableAjaxResponse
            {
                draw = request.draw,
                recordsTotal = query.Count()
            };

            var allProps = query.ElementType.GetProperties();
            var stringProps = allProps.Where(p => p.PropertyType == typeof(string));

            if (request.columns != null)
            {
                var searchedcols = request.columns
                    .Where(p => p.search.value != null)
                    .ToList();

                if (request.search.value != null || searchedcols.Count > 0)
                {
                    var prms = new List<object>();
                    var x = 0;
                    var str = "";
                    var searchTypes = new List<Type>
                    {
                        typeof(string),
                        typeof(int),
                        typeof(int?),
                        typeof(decimal),
                        typeof(decimal?),
                        typeof(bool),
                        typeof(bool?),
                        typeof(DateTime),
                        typeof(DateTime?)
                    };

                    if (request.search.value != null)
                        foreach (var prop in allProps)
                        {
                            var col = request.columns.FirstOrDefault(p => p.data == prop.Name);

                            if (col == null || !col.searchable || !searchTypes.Contains(prop.PropertyType))
                                continue;

                            var strPart = prop.PropertyType != typeof(string) ? ".ToString()" : "";
                            str += (str == "" ? str : " or ") + $"{prop.Name + strPart}.Contains(@{x++})";
                            prms.Add(request.search.value);
                        }

                    str = str != "" ? $"({str})" : str;

                    foreach (var col in searchedcols)
                        if (col.search.value != null)
                        {
                            var prop = allProps.First(p => p.Name == col.data);
                            var strPart = prop.PropertyType != typeof(string) ? ".ToString()" : "";
                            str += (str == "" ? str : " and ") + $"{col.data + strPart}.Contains(@{x++})";
                            prms.Add(col.search.value);
                        }

                    query = query.Where(str, prms.ToArray());
                }
            }

            response.recordsFiltered = query.Count();

            if (request.order != null && request.order.Count > 0)
            {
                var str = "";

                foreach (var order in request.order)
                    if (request.columns[order.column].orderable)
                        str += (str == "" ? str : ",") + $"{request.columns[order.column].data} {order.dir}";

                if (str != "")
                    query = query.OrderBy(str);
            }

            response.data = (request.length >= 0
                ? query
                    .Skip(request.start)
                    .Take(request.length)
                    .ToList()
                : query
                    .Skip(request.start)
                    .ToList());

            return await Task.FromResult(response);
        }
    }
}