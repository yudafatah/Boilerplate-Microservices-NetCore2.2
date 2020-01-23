using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login req)
        {
            ServiceResponseSingle<LoginRes> res = new ServiceResponseSingle<LoginRes>();

            var Token = _userManager.Authenticate(req.Username, req.Password);
            if (Token == null)
            {
                res.Code = -1;
                res.Message = "Username or password is incorrect";
                return new OkObjectResult(res);
            }

            var logRes = new LoginRes()
            {
                JWT_Token = Token
            };

            res.Code = 1;
            res.Message = "success";
            res.Data = logRes;

            return new OkObjectResult(res);
        }
    }
}