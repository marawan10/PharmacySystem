using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.DTOs
{
    public class GetUserDto
    {
        public string firstname {  get; set; }
        public string lastname {  get; set; }
        public string UserName { get; set; }
        public string email { get; set; }

        public string Password { get; set; }
    }
}
