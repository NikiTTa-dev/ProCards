using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProCards.Web.Logic;

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
        
        if (error is ValidationException er)
            return new BadRequestObjectResult(
                ConcatList.ConcatListOfString(er.Errors.Select(e => e.ErrorMessage).ToList()));
        return new BadRequestObjectResult("Unhandled error was occured!");
    }
}