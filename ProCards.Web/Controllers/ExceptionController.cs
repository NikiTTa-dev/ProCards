using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProCards.DAL.Exceptions;

namespace ProCards.Web.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        var error = HttpContext.Features
            .Get<IExceptionHandlerPathFeature>()
            ?.Error;
        
        if (error is CardNotFoundException)
            return new NotFoundObjectResult(error.Message);
        if (error is CardAlreadyExistsException)
            return new BadRequestObjectResult(error.Message);
        return new BadRequestObjectResult("Unhandled error was occured!");
        
    }
}
