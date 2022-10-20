using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Authorization
{
    public class AdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Movie>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Movie movie)
        {
            if(context.User == null || movie == null)
                return Task.CompletedTask;

            if(context.User.IsInRole(Constraints.AdminRole))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
