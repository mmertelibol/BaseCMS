using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDtos
{
   public  class RoleAssignDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Exists { get; set; }
    }
}
