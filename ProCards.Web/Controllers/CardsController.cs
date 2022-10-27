using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProCards.Infrastructure.Data;
using ProCards.Infrastructure.Models;

namespace ProCards.Web.Controllers;

[ApiController]
[Route("cards")]
public class CardsController : ControllerBase
{
    private readonly ILogger<CardsController> _logger;

    public CardsController(ILogger<CardsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllCards()
    {
        using var dbContext = HttpContext.RequestServices.GetRequiredService<AppDbContext>();
        try
        {
            var persons = dbContext.Cards?.ToList();
            return Ok(persons);
        }
        catch (Exception ex)
        {       
            _logger.LogError(ex, "Not found. {ExceptionMessage}", ex.Message);
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult PostCard([FromBody] Card card)
        
    {
        try
        {
            using (var dbContext = HttpContext.RequestServices.GetRequiredService<AppDbContext>())
            {
                Card carddd = new Card
                {
                    FirstSide = "a", SecondSide = "a", CategoryId = 0
                };

                dbContext.Cards?.Add(card);
                dbContext.SaveChanges();
            }
        }
        catch (Exception ex)
        {
        }

        return Ok();
    }
}