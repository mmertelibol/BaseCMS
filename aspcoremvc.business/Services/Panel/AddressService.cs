using AutoMapper;
using Business.Services.Panel.Interfaces;
using Common.Dto.PanelDto;
using Data;
using Data.Domain.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services.Panel
{
    public class AddressService:IAddressService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AddressService(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public AddressDto AddAddress(AddressDto addressDto)
        {
            addressDto.AddedDate = DateTime.Now;
            var address = _mapper.Map<Address>(addressDto);
            var added = _context.Adress.Add(address);
            var dtoModel = _mapper.Map<AddressDto>(address);

            _context.SaveChanges();
            return dtoModel;

        }

        public AddressDto DeleteAddress(int addressId)
        {
            var address = _context.Adress.Find(addressId);
            var deletedAddress = _context.Adress.Remove(address);
            var dtoModel = _mapper.Map<AddressDto>(deletedAddress);

            _context.SaveChanges();
            return dtoModel;

        }

        public AddressDto GetAddressById(int addressId)
        {
            var address = _context.Adress.Find(addressId);

            var dtoModel = _mapper.Map<AddressDto>(address);

            return dtoModel;
        }

        public List<AddressDto> GetAllAddress()
        {
            var addressList = _context.Adress.Where(x => x.IsDeleted == false).ToList();
            var dtoModel = _mapper.Map<List<AddressDto>>(addressList);

            return dtoModel;
        }

        public AddressDto UpdateAddress(AddressDto addressDto)
        {
            var address = _context.Adress.Find(addressDto.Id);
            address.Id = addressDto.Id;
            address.Latitude = addressDto.Latitude;
            address.Longitude = addressDto.Longitude;
            address.Name = addressDto.Name;
            address.Phone = addressDto.Phone;
            address.UpdatedDate = DateTime.Now;
            address.FullAdress = addressDto.FullAdress;

            _context.Adress.Update(address);
            _context.SaveChanges();

            return addressDto;
        }
    }
}
