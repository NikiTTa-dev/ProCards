using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    private IMapper _mapper;
    private ICardRepository _cardRepository;

    public CardController(
        IMapper mapper,
        ICardRepository cardRepository)
    {
        _mapper = mapper;
        _cardRepository = cardRepository;
    }

    [HttpPost]
    public async Task<IActionResult> PostCard([FromBody] CardDto card)
    {
        var cardDal = _mapper.Map<CardDal>(card);
        await _cardRepository.InsertCardWithCategoryAsync(cardDal);
        await _cardRepository.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetCards(
        [FromQuery] string name,
        [FromQuery] bool isUser,
        [FromQuery] [Range(1, 20)] int count)
    {
        var cards = await _cardRepository.GetCardsAsync(name, isUser, count);
        List<CardDto> cardDtos = cards
            .Select(card => _mapper.Map<CardDto>(card))
            .ToList();
        return Ok(cardDtos);
    }

    [HttpPost("new")]
    public async Task<IActionResult> GetNewCardCollectionAsync(
        [FromServices] GradesLogic logic,
        [FromBody] [LimitCount(1, 20)] List<CardDto> cardDtos)
    {
        var result = await logic.GetNewCardsByGrades(cardDtos);
        return Ok(result);
    }
}