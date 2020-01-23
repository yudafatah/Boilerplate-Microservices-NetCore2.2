using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.WebApi.Catalog.Services;
using Service.WebApi.Catalog.ViewModels;

namespace Service.WebApi.Catalog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        public CatalogController()
        {
            //_tokenManager = tokenManager;
        }

        [Authorize(Roles = AuthConst.Admin)]
        [HttpGet]
        public IActionResult GetAdminData()
        {
            return new OkObjectResult(new ServiceResponseSingle<string>()
            {
                Code = 1,
                Message = "success",
                Data = "Hi I'm a " /*+ _tokenManager.GetPrincipal().Role*/
            });
        }

        [Authorize(Policy = AuthConst.Department_IT)]
        [HttpGet]
        public IActionResult GetDepartmentITData()
        {
            return new OkObjectResult(new ServiceResponseSingle<string>()
            {
                Code = 1,
                Message = "success",
                Data = "Hi I'm work in " /*+ _tokenManager.GetPrincipal().Department + " department"*/
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetData()
        {
            List<Product_VM> res = new List<Product_VM>()
            {
                new Product_VM {Id=1,Name="Coffee", Price="5 $", Stock = 10},
                new Product_VM {Id=2, Name="Indomie", Price = "1 $", Stock = 50}
            };

            return new OkObjectResult(new ServiceResponse<Product_VM>()
            {
                Code = 1,
                Message = "success",
                Data = res
            });
        }
    }
}