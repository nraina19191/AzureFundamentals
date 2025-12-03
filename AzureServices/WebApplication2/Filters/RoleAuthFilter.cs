using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication2.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleAuthFilter :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.Headers.Append("Role-Auth-Filter", "Executed");
        }
    }
}
