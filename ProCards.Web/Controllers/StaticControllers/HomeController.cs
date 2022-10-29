using Microsoft.AspNetCore.Mvc;

namespace ProCards.Web.Controllers.StaticControllers;

[ApiController]
[Route("/{controller}/{action}")]
public class HomeController: ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var html = System.IO.File.ReadAllText(@"./Views/index.html");

        return Content(html, "text/html");
    }

    [HttpGet("/style.css")]
    public IActionResult GetCss()
    {
        var css = System.IO.File.ReadAllText(@"./wwwroot/css/style.css");

        return Content(css, "text/css");
    }

    [HttpGet("/main.js")]
    public IActionResult GetJs()
    {
        var js = System.IO.File.ReadAllText(@"./wwwroot/js/main.js");

        return Content(js, "application/js");
    }
}