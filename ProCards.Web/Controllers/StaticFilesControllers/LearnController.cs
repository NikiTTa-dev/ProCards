using Microsoft.AspNetCore.Mvc;

namespace ProCards.Web.Controllers.StaticFilesControllers;

[Route("/learn")]
[Controller]
public class LearnController: ControllerBase
{
    [HttpGet]
    public IActionResult Create()
    {
        return File("~/learn.html", "text/html");
    }
}