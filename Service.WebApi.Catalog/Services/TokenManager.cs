using Microsoft.AspNetCore.Http;
using Service.WebApi.Catalog.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Service.WebApi.Catalog.Services
{
    public interface ITokenManager
    {
        Principal_VM GetPrincipal();
    }
    public class TokenManager : ITokenManager
    {
        private IHttpContextAccessor _context;
        public TokenManager(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor)); ;
        }
        public Principal_VM GetPrincipal()
        {
            string authorization = _context.HttpContext.Request.Headers["Authorization"];
            if (authorization != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = authorization.Split(" ")[1];
                var parsedToken = tokenHandler.ReadJwtToken(token);

                var department = parsedToken.Claims.Where(c => c.Type == "department").FirstOrDefault();

                var employeeid = parsedToken.Claims.Where(c => c.Type == "employeeid").FirstOrDefault();

                var role = parsedToken.Claims.Where(c => c.Type == "role").FirstOrDefault();

                var exp_time = parsedToken.Claims.Where(c => c.Type == "exp").FirstOrDefault();

                return new Principal_VM()
                {
                    Department = department.Value,
                    Employee_Id = employeeid.Value,
                    Role = role.Value,
                    Expired_Time = exp_time.Value
                };
            }

            else
            {
                return null;
            }
        }
    }
}
