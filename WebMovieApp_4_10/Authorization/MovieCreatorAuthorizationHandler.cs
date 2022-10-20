using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using WebMovieApp_4_10.Data;
using WebMovieApp_4_10.Exstension;
using WebMovieApp_4_10.Models;

namespace WebMovieApp_4_10.Authorization
{
    public class MovieCreatorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Movie>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public MovieCreatorAuthorizationHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Movie movie)
        {
            if(context.User == null || movie == null)
                return Task.CompletedTask;

            if(requirement.Name != Constraints.CreateOperationName &&
                requirement.Name != Constraints.ReadOperationName &&
                requirement.Name != Constraints.DeleteOperationName &&
                requirement.Name != Constraints.UpdateOperationName)
            {
                return Task.CompletedTask;
            };

            if(movie.CreatorId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            };

            return Task.CompletedTask;
        }
    }
}
