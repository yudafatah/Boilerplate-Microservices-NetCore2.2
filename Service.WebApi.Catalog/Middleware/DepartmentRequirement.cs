using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.WebApi.Catalog.Middleware
{
    public class DepartmentRequirement : IAuthorizationRequirement
    {
        public string _role { get; }

        public DepartmentRequirement(string role)
        {
            _role = role;
        }
    }
}
