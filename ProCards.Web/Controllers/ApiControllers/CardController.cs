using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
    public IActionResult PostCard(
        [FromBody] CardDto card,
        [FromServices] ICardRepository cardRepository,
        [FromServices] IMapper mapper
        )
    {
        try
        {
            var cardDal = mapper.Map<CardDal>(card);
            cardRepository.InsertCardWithCategory(cardDal);
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
    public IActionResult GetCards(
        [FromServices] ICardRepository cardRepository,
        [FromServices] IMapper mapper,
        [FromQuery] string name,
        [FromQuery] bool isUser,
        [FromQuery] int count)
    {
        List<CardDal> card;
        try
        {
            card = cardRepository.GetCards(name, isUser, count);
            var cardDto = mapper.Map<CardDto>(card.FirstOrDefault());
            return Ok(cardDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred getting card. {ExceptionMessage}", ex.Message);
        }

        return BadRequest("No cards in this category or server error.");
    }

    [HttpGet("new")]
    public IActionResult GetNewCardCollection([FromBody] List<CardDto> cardDtos)
    {
        return Ok();
    }
}