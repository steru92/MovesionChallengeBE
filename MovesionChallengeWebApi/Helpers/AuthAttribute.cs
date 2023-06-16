using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovesionChallengeWebApi.Entities;

namespace MovesionChallengeWebApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items[nameof(User)];
            if (user == null)
            {
                /// not logged in
                context.Result = new JsonResult(new { message = Constants.Constants.ERROR_MESSAGE_UNAUTHORIZED }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}