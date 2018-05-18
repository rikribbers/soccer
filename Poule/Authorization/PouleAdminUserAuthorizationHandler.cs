using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Poule.Models;

namespace Poule.Authorization
{
    public class PouleAdminUserAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, User>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            User resource)
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
