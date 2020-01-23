using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.WebMVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAdminData()
        {
            //var response = await client.GetAsync($"/account/balance");
            //return await DeserializeResponseContent(response);
        }
    }
}