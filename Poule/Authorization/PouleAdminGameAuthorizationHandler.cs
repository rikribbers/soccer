﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Poule.Models;

namespace Poule.Authorization
{
    public class PouleAdminGameAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Game>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Game resource)
        {
            if (context.User == null)
                return Task.CompletedTask;

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.PouleAdministratorsRole))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
