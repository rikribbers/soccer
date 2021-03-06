﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Poule.Models;

namespace Poule.Authorization
{
    public class PouleGameManagersAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Game>
    {
        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                OperationAuthorizationRequirement requirement,
                Game resource)
        {
            if (context.User == null || resource == null)
                return Task.CompletedTask;

            if (context.User.IsInRole(Constants.PouleGameManagersRole))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
