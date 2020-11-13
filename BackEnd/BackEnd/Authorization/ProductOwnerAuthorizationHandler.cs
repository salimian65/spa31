using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BackEnd.Authorization
{
    public class ProductOwnerAuthorizationHandler : AuthorizationHandler<ProductOwnerAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ProductOwnerAuthorizationRequirement requirement
            //, Product resource
            )
        {
            if (!context.User.HasClaim(c => c.Type == JwtClaimTypes.Name && c.Issuer == "http://localhost:5000"))
                return Task.CompletedTask;

            var role = context.User
                .FindFirst(c => c.Type == JwtClaimTypes.Role && c.Issuer == "http://localhost:5000").Value;

            if (context.Resource is Endpoint endpoint)
            {
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
               
            }

            if (role == "customer")
                context.Succeed(requirement);

            return Task.CompletedTask;
        }


        //public class PermissionHandler : IAuthorizationHandler
        //{
        //    public Task HandleAsync(AuthorizationHandlerContext context)
        //    {
        //        var pendingRequirements = context.PendingRequirements.ToList();

        //        foreach (var requirement in pendingRequirements)
        //        {
        //            if (requirement is ReadPermission)
        //            {
        //                if (IsOwner(context.User, context.Resource) ||
        //                    IsSponsor(context.User, context.Resource))
        //                {
        //                    context.Succeed(requirement);
        //                }
        //            }
        //            else if (requirement is EditPermission ||
        //                     requirement is DeletePermission)
        //            {
        //                if (IsOwner(context.User, context.Resource))
        //                {
        //                    context.Succeed(requirement);
        //                }
        //            }
        //        }

        //        //TODO: Use the following if targeting a version of
        //        //.NET Framework older than 4.6:
        //        //      return Task.FromResult(0);
        //        return Task.CompletedTask;
        //    }

        //    public object DeletePermission { get; set; }

        //    public object EditPermission { get; set; }

        //    public object ReadPermission { get; set; }

        //    private bool IsOwner(ClaimsPrincipal user, object resource)
        //    {
        //        // Code omitted for brevity

        //        return true;
        //    }

        //    private bool IsSponsor(ClaimsPrincipal user, object resource)
        //    {
        //        // Code omitted for brevity

        //        return true;
        //    }
        //}



        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            return base.HandleAsync(context);
        }
    }

    public class MyHandler1: IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {   //context.Succeed(new ClaimsAuthorizationRequirement());
          //  throw new System.NotImplementedException();
            return Task.CompletedTask;
        }
    }

    public class AuthorizationService : IAuthorizationService 
    {
       // private ContextFactory _contextFactory;

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            // Create a tracking context from the authorization inputs.
            //var authContext = _contextFactory.CreateContext(requirements, user, resource);

            //// By default this returns an IEnumerable<IAuthorizationHandlers> from DI.
            //var handlers = await _handlers.GetHandlersAsync(authContext);

            //// Invoke all handlers.
            //foreach (var handler in handlers)
            //{
            //    await handler.HandleAsync(authContext);
            //}

            //// Check the context, by default success is when all requirements have been met.
            //return _evaluator.Evaluate(authContext);
            throw new System.NotImplementedException();
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            throw new System.NotImplementedException();
        }
    }
}