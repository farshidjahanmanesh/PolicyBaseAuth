using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBaseAuth.Auth
{
    public static class Permissions
    {
        
        public const string basePermission = "BasePermission";
        public const string readBook = "ReadBook";
        public const string Admin = "Admin";
        public const string Editor = "Editor";

        public static AuthorizationPolicy BasePermission(this AuthorizationPolicyBuilder builder)
        {
            return builder.RequireClaim("BasePermission").Build();
        }

        public static AuthorizationPolicy ReadBook(this AuthorizationPolicyBuilder builder)
        {
            return builder.AddRequirements(new ReadBookRequirement()).Build();
        }
    }
}
