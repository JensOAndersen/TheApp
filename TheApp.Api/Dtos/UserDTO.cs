using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheApp.Api.Entities;

namespace TheApp.Api.Dtos
{
    public class UserDTOIn : User
    {
        public string Password { get; set; }
    }

    public class UserDTOOut : User
    {
        public string Token { get; set; }
    }
}
