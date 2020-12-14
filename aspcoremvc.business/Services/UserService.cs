using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Services.Interfaces;
using Common.Dto;
using Common.Dto.DataTablesGrid;
using Common.Resources;
using Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly AppDbContext _context;
        private readonly LocService _localizer;

        public UserService(AppDbContext parContext, LocService parLocalizer)
            : base(parContext, parLocalizer)
        {
            _context = parContext;
            _localizer = parLocalizer;
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

     
    }
}