using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebMovieApp_4_10.Authorization
{
    public class MovieOperation
    {
        public static OperationAuthorizationRequirement Create = 
            new OperationAuthorizationRequirement { Name = Constraints.CreateOperationName };

        public static OperationAuthorizationRequirement Read = 
            new OperationAuthorizationRequirement { Name = Constraints.ReadOperationName };

        public static OperationAuthorizationRequirement Update = 
            new OperationAuthorizationRequirement { Name = Constraints.UpdateOperationName };

        public static OperationAuthorizationRequirement Delete = 
            new OperationAuthorizationRequirement { Name = Constraints.DeleteOperationName };
    }
}
