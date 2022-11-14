using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProCards.Web.Filters;

public class CategoriesActionFilterAttribute: ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Response.Headers.Add("is-last", "false");
        await next.Invoke();
    }
}