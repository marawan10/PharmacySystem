using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.DTOs
{
    public class CreateUserDto
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

    }
}
