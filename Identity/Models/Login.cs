using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginRes
    {
        public string JWT_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
