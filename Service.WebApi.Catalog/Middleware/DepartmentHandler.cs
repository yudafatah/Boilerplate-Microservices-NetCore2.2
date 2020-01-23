using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.WebApi.Catalog.Middleware
{
    public class DepartmentHandler : AuthorizationHandler<DepartmentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DepartmentRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "role" &&
                                        c.Issuer == "https://yudafatah.com"))
            {
                //TODO: Use the following if targeting a version of
                //.NET Framework older than 4.6:
                //      return Task.FromResult(0);
                return Task.CompletedTask;
            }

            var role = context.User.FindFirst(c => c.Type == "role" &&
                                            c.Issuer == "http://contoso.com").Value;

            if (role == requirement._role)
            {
                context.Succeed(requirement);
            }

            //TODO: Use the following if targeting a version of
            //.NET Framework older than 4.6:
            //      return Task.FromResult(0);
            return Task.CompletedTask;
        }
    }
}
