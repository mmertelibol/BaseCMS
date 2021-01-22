using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Services.Interfaces;
using Common.Dto;
using Common.Dto.DataTablesGrid;
using Common.Dto.PanelDtos;
using Common.Resources;
using Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly AppDbContext _context;
        private readonly LocService _localizer;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(AppDbContext parContext, LocService parLocalizer, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
            : base(parContext, parLocalizer)
        {
            _context = parContext;
            _localizer = parLocalizer;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserDto GetUser(int id)
        {
            var user = _context.Users
                .Where(usr => !usr.IsDeleted && usr.IsActive && usr.Id == id)
                .ProjectTo<UserDto>().FirstOrDefault();

            return user;
        }

        public UserDto CheckUserPhone(string phone, int locationId)
        {

            var user = _context.Users.Where(x => x.PhoneNumber.Equals(phone) && !x.IsDeleted).FirstOrDefault();
            var mappedResult = new UserDto();

            try
            {
                mappedResult = Mapper.Map<UserDto>(user);
            }
            catch {/*Ignore*/}

            return mappedResult;
        }

        public async Task<bool> Login(UserDto userdDto)
        {
            var email = await _userManager.FindByEmailAsync(userdDto.Email);

            var identityresult = await _signInManager.PasswordSignInAsync(email, userdDto.Password, false, false);

            var success = identityresult.Succeeded;


            return success;
        }

        public async Task<bool> SignOut()
        {
            await  _signInManager.SignOutAsync();


            return true;
        }

        public List<UserDto> GetAllUser()
        {
            var userList = _context.Users.Where(x => !x.IsDeleted && x.IsActive).ToList();
            var dtoModel = Mapper.Map<List<UserDto>>(userList);

            return dtoModel;
        }

        public async Task<List<UserDto>> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            List<UserDto> roleList = new List<UserDto>();
            foreach (var item in roles)
            {
                UserDto role = new UserDto();
                role.RoleId = item.Id;
                role.RoleName = item.Name;
                role.Exists = userRoles.Contains(item.Name);
                roleList.Add(role);
            }
            return roleList;
        }

        public async Task<List<UserDto>> AssignRolePost(List<UserDto> assignRoles, int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);

            foreach (var item in assignRoles)
            {
                if (item.Exists)
                {
                   await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return assignRoles;
        }

      
    }
}