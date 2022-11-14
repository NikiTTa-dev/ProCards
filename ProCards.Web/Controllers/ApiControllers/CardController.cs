using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;
using ProCards.Web.Attributes;
using ProCards.Web.Data.DTOs;
using ProCards.Web.Logic;

namespace ProCards.Web.Controllers.ApiControllers;

[ApiController]
[Route("cards")]
public class CardController : ControllerBase
{
    private ILogger<CardController> _logger;
    private IMapper _mapper;
    private ICardRepository _cardRepository;

    public CardController(
        ILogger<CardController> logger,
        IMapper mapper,
        ICardRepository cardRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _cardRepository = cardRepository;
    }

    [HttpPost]
    public async Task<IActionResult> PostCard([FromBody] CardDto card)
    {
        //try
        //{
            var cardDal = _mapper.Map<CardDal>(card);
            await _cardRepository.InsertCardWithCategoryAsync(cardDal);
            await _cardRepository.SaveAsync();
            return NoContent();
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "An error occurred creating card. {ExceptionMessage}", ex.Message);
        // }

        return BadRequest("An error occurred creating card. Invalid card.");
    }

    [HttpGet]
    public async Task<IActionResult> GetCards(
        [FromQuery] string name,
        [FromQuery] bool isUser,
        [FromQuery]
        [Range(1, 20)]
        int count)
    {
        //try
        //{
            var cards = await _cardRepository.GetCardsAsync(name, isUser, count);
            List<CardDto> cardDtos = cards
                .Select(card => _mapper.Map<CardDto>(card))
                .ToList();
            return Ok(cardDtos);
        //}
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "An error occurred getting card. {ExceptionMessage}", ex.Message);
        // }

        return BadRequest("No cards in this category or server error.");
    }

    [HttpPost("new")]
    public async Task<IActionResult> GetNewCardCollectionAsync(
        [FromServices] GradesLogic logic,
        [FromBody]
        [LimitCount(1, 20)]
        List<CardDto> cardDtos)
    {
        await logic.GetNewCardsByGrades(cardDtos);
        return Ok();
    }
}