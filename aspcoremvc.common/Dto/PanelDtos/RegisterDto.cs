using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.PanelDtos
{
    public class RegisterDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RePassword { get; set; }

        public string Email { get; set; }



    }
}
