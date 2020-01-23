using Identity.Models;
using Identity.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IUserManager
    {
        string Authenticate(string username, string password);
    }
    public class UserManager : IUserManager
    {
        private List<User> _users = new List<User>
        {
            new User { Department = "IT", Email = "fatahyuda@gmail.com", Employee_Id = "1234", Username = "yuda", Password = "yuda@123", Role="SuperAdmin" },
            new User { Department = "Finance", Email = "admin@gmail.com", Employee_Id = "5678", Username = "admin", Password = "admin@123", Role="Admin" },
            new User { Department = "Sales", Email = "user@gmail.com", Employee_Id = "9876", Username = "user", Password = "user@123", Role="User" },
        };

        private readonly CredentialAttr _appSettings;

        public UserManager(IOptions<CredentialAttr> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("department", user.Department),
                    new Claim("employeeid", user.Employee_Id),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return jwtSecurityToken;
        }
    }
}
