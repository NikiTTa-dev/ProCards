﻿using Microsoft.AspNetCore.Mvc;

namespace ProCards.Web.Controllers.StaticControllers;

[ApiController]
[Route("/")]
public class IndexController: ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return File("~/index.html", "text/html");
    }
}