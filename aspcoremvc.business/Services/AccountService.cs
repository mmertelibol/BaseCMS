using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Services.Interfaces;
using Common;
using Common.Dto;
using Common.Dto.DataTablesGrid;
using Common.Resources;
using Data;
using Data.Domain;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NPOI.OpenXmlFormats.Dml;
using Business.Services;
using Common.Helpers;

namespace Business.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        #region ctor
        private readonly AppDbContext _context;
        private readonly CacheBase _cache;
        private readonly AppDbContextSystem _contextSystem;
        protected readonly LocService _localizer;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;



        public AccountService(AppDbContext context, AppDbContextSystem contextSystem, LocService localizer,
        UserManager<User> userManager, CacheBase cacheService, RoleManager<Role> roleManager)
            : base(context, localizer)
        {
            _context = context;
            _contextSystem = contextSystem;
            _localizer = localizer;
            _userManager = userManager;
            _cache = cacheService;
            _roleManager = roleManager;
        }
        #endregion


        public int AddActionLog(ActionLogDto dto)
        {
            ActionLog dbitem = Mapper.Map<ActionLog>(dto);
            _contextSystem.ActionLogs.Add(dbitem);
            _contextSystem.SaveChanges();

            return dbitem.Id;
        }

        public void UpdateActionLog(int id, bool hasException)
        {
            var endTime = DateTime.Now;
            var dbItem = _contextSystem.ActionLogs.Find(id);

            dbItem.HasException = hasException;
            dbItem.RequestEnd = endTime;
            dbItem.ElapsedTimeInSeconds = (endTime - dbItem.RequestStart).Milliseconds / 1000m;

            _contextSystem.SaveChanges();
        }

        public List<PageDto> GetAllMenu()
        {
            var menu = new List<PageDto>();
            menu = Mapper.Map<List<PageDto>>(_context.Pages.Where(p => !p.IsDeleted).ToList());

            return menu;
        }

        public List<Role> GetUserRoles(int userId)
        {
            var roleIds = _context.UserRoles
                .Where(p => p.UserId == userId)
                .Select(p => p.RoleId)
                .ToList();

            var roles = _context.Roles
                .Where(p => roleIds.Contains(p.Id))
                .ToList();

            return roles;
        }

        public List<RoleDto> GetAllRoles()
        {
            var roles = _context.Roles.Where(x => x.Id != 1 && x.IsActive)
                .Select(p => new RoleDto
                {
                    RoleId = p.Id,
                    RoleName = p.Name
                })
                .ToList();

            return roles;
        }

        public async Task<DataTableAjaxResponse> GetUserAndRoles(UserRolePostDto request)
        {
            var query = from ur in _context.UserRoles
                        join u in _context.Users on ur.UserId equals u.Id
                        join r in _context.Roles on ur.RoleId equals r.Id
                        //where post.ID == id
                        group new { ur, u, r } by ur.UserId into g
                        select new UserRoleDto
                        {
                            DT_RowId = $"{g.Key}",
                            UserId = g.Key,
                            UserName = g.FirstOrDefault().u.UserName,
                            Roles = g.Select(p => new RoleDto
                            {
                                RoleId = p.r.Id,
                                RoleName = p.r.Name
                            })
                        };

            var result = await query.ApplyGridBase(request);
            (result.data as List<UserRoleDto>)?.ForEach(p => p.RolesToString = string.Join(",", p.Roles.Select(x => x.RoleName)));

            return result;
        }

        public async Task<bool> CheckPhoneNumber(string phone)
        {
            var guest = await _context.Users.Where(x => x.PhoneNumber.Equals(phone) && x.IsDeleted == false).FirstOrDefaultAsync();
            return guest != null;
        }

        public async Task<bool> CheckUserEmail(string email)
        {
            var guest = await _context.Users.Where(x => x.Email.Equals(email) && x.IsDeleted == false).FirstOrDefaultAsync();
            return guest != null;
        }

        public UserRoleDto GetUserRole(int userId)
        {
            var query = from ur in _context.UserRoles
                        join u in _context.Users.Where(x => x.Id == userId) on ur.UserId equals u.Id
                        join r in _context.Roles on ur.RoleId equals r.Id
                        group new { ur, u, r } by ur.UserId into g
                        select new UserRoleDto
                        {
                            DT_RowId = $"{g.Key}",
                            UserId = g.Key,
                            UserName = g.FirstOrDefault().u.UserName,
                            Email = g.FirstOrDefault().u.Email,
                            Roles = g.Select(p => new RoleDto
                            {
                                RoleId = p.r.Id,
                                RoleName = p.r.Name
                            })
                        };

            var user = query.FirstOrDefault();

            if (user == null)
            {
                user = _context.Users.Where(x => x.IsDeleted == false && x.IsActive == true).ProjectTo<UserRoleDto>().FirstOrDefault();
                if (user != null)
                {
                    user.RolesToString = _localizer.Get("Mevcut bir rol yok");
                    user.Roles = new List<RoleDto>();
                }
            }
            else
            {

                var roles = user.Roles.ToList();
                for (int i = 0; i < roles.Count; i++)
                {
                    user.RolesToString += roles[i].RoleName;
                    if (roles.Count != i + 1) user.RolesToString += ",";
                }
            }

            return user;
        }

        public async Task<Page> FindPageAsync(int id)
        {
            return await _context.Pages.FindAsync(id);
        }

        public async Task<JsonResultDto> ResetPassword(string username, string email)
        {
            JsonResultDto result = new JsonResultDto();
            User user = null;

            if (!string.IsNullOrWhiteSpace(username)) user = await _userManager.FindByNameAsync(username);
            if (!string.IsNullOrWhiteSpace(email)) user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            result.DataObject = token;
            result.Status = string.IsNullOrWhiteSpace(token) ? JsonResultStatus.Warning : JsonResultStatus.Success;
            result.Message = result.Status.ToString();
            return result;
        }

        public async Task<JsonResultDto> SetPassword(string token, string username, string email, string newPassword)
        {
            JsonResultDto result = new JsonResultDto();
            User user = null;

            if (!string.IsNullOrWhiteSpace(username)) user = await _userManager.FindByNameAsync(username);
            if (!string.IsNullOrWhiteSpace(email)) user = await _userManager.FindByEmailAsync(email);

            var identityResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (identityResult.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            result.DataObject = identityResult.Succeeded;
            result.Status = identityResult.Succeeded ? JsonResultStatus.Success : JsonResultStatus.Warning;
            result.Message = identityResult.Errors.Count() > 0 ? string.Join(",", identityResult.Errors.Select(x => x.Description)) : "Success";

            return result;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return Mapper.Map<UserDto>(user);
        }

        public bool LogoutUser()
        {
            return _cache.RemoveCache("Genel.AllMenu");
        }

        public async Task<RoleDto> AddRole(RoleDto roleDto)
        {
            Role role = new Role()
            {
                Name = roleDto.RoleName,
                IsDeleted = false,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                StartPageId = 1

                
            };

             await _roleManager.CreateAsync(role);

            return roleDto;
        }

    }
}