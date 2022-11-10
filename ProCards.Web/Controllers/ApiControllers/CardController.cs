using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Controllers.ApiControllers;

[ApiController]
[Route("cards")]
public class CardController : ControllerBase
{
    private ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult PostCard([FromBody] CardDal card, [FromServices] ICardRepository cardRepository)
    {
        try
        {
            cardRepository.InsertCardWithCategory(card);
            cardRepository.Save();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred creating card. {ExceptionMessage}", ex.Message);
        }

        return BadRequest("An error occurred creating card. Invalid card.");
    }

    [HttpGet]
    public IActionResult GetCard([FromServices] ICardRepository cardRepository, [FromQuery] string name,
        [FromQuery] bool isUser, [FromQuery] int count)
    {
        List<CardDal> card;
        try
        {
            card = cardRepository.GetCards(name, isUser, count);
            return Ok(card);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred getting card. {ExceptionMessage}", ex.Message);
        }

        return BadRequest("No cards in this category or server error.");
    }
}