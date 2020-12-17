using Common.Dto.PanelDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface  IAddressService
    {
        List<AddressDto> GetAllAddress();
        AddressDto GetAddressById(int addressId);

        AddressDto DeleteAddress(int addressId);

        AddressDto AddAddress(AddressDto addressDto);

        AddressDto UpdateAddress(AddressDto addressDto);



    }
}
