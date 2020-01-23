using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.WebMVC.ViewModels
{
    public class LoginRes
    {
        public string JWT_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
