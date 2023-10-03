using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ToDo.Filters
{
    public class UserIdAsyncFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string currentUserId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                context.HttpContext.Items["CurrentUserId"] = currentUserId;
            }

            await next.Invoke();
        }
    }
}
