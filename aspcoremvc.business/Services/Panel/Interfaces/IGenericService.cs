using Common.Dto.PanelDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Panel.Interfaces
{
    public interface IGenericService
    {
        GenericDto GetAllDto(GenericDto genericDto);
        
    }
}
