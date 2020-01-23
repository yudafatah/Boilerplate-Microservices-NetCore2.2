using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.WebMVC.Const;
using Client.WebMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Client.WebMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServiceHost _host;
        private readonly HttpClient _client;
        public LoginController(IOptions<ServiceHost> options)
        {
            _client = new HttpClient();
            _host = options.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] Login_VM req)
        {
            var response = await _client.PostAsJsonAsync($"/Account/Login", new { Username = req.Username, Password = req.Password });
            var stringRes = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ServiceResponseSingle<LoginRes>>(stringRes);
            if (res != null)
            {
                HttpContext.Session.SetString(SessionAttr.JWT_Token, res.Data.JWT_Token);
                HttpContext.Session.SetString(SessionAttr.Refresh_Token, res.Data.Refresh_Token);

                return View("Menu");
            }
            else
            {
                return View();
            }
        }
    }
}