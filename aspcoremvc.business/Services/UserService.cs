using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Services.Interfaces;
using Common.Dto;
using Common.Dto.DataTablesGrid;
using Common.Resources;
using Data;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using System;
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

        public UserService(AppDbContext parContext, LocService parLocalizer, SignInManager<User> signInManager, UserManager<User> userManager)
            : base(parContext, parLocalizer)
        {
            _context = parContext;
            _localizer = parLocalizer;
            _signInManager = signInManager;
            _userManager = userManager;
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

        public async Task<bool> Login(UserDto user)
        {
            var email = await _userManager.FindByEmailAsync(user.Email);

            var identityresult = await _signInManager.PasswordSignInAsync(email, user.Password, false, false);

            var success = identityresult.Succeeded;


            return success;
        }

        public async Task<bool> SignOut()
        {
            await  _signInManager.SignOutAsync();


            return true;
        }
    }
}