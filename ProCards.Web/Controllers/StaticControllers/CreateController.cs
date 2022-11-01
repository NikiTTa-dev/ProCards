﻿using Microsoft.AspNetCore.Mvc;

namespace ProCards.Web.Controllers.StaticControllers;

[Route("/create")]
[Controller]
public class CreateController: ControllerBase
{
    [HttpGet]
    public IActionResult Create()
    {
        return File("~/create.html", "text/html");
    }
}