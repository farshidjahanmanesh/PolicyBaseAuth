using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBaseAuth.Auth
{
    public class ReadBookRequirement : IAuthorizationRequirement
    {
    }

    public class AdminReadBookHandler : AuthorizationHandler<ReadBookRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadBookRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Value == Permissions.Admin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class EditorReadBookHandler : AuthorizationHandler<ReadBookRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadBookRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Value == Permissions.Editor))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
