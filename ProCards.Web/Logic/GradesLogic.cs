using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Models;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Logic;

public class GradesLogic
{
    private IGradeRepository _gradeRepository;
    private ICardRepository _cardRepository;
    private IMapper _mapper;

    public GradesLogic(IGradeRepository gradeRepository, IMapper mapper, ICardRepository cardRepository)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
        _cardRepository = cardRepository;
    }

    public async Task<List<CardDto>> GetNewCardsByGrades(List<CardDto> cards)
    {
        await InsertGradesToDb(cards);

        var orderedCards = cards
            .OrderBy(card => card.Grades.Last())
            .ToList();

        var category = orderedCards.First().Category;

        var removedCount = orderedCards.RemoveAll(card => card.Grades.Count(grade => grade == 5 || grade == 4) > 1);

        var newCardsCount = orderedCards.Count(card => card.Grades.Contains(5) || card.Grades.Contains(4)) -
                            removedCount;
        if (newCardsCount + orderedCards.Count < 5)
            newCardsCount = 5 - (newCardsCount + orderedCards.Count);
        if (newCardsCount + orderedCards.Count > 20)
            newCardsCount = 20 - orderedCards.Count;

        var insertingIndex = orderedCards.FindIndex(card => card.Grades.Last() == 4 || card.Grades.Last() == 5);
        insertingIndex = insertingIndex == -1 ? orderedCards.Count - 1 : insertingIndex;

        for (int i = 0; i < newCardsCount; i++)
        {
            orderedCards.Insert(insertingIndex, _mapper.Map<CardDto>(
                await _cardRepository.GetCardByCategoryAsync(category.Name, category.IsUserCategory ?? false)));
        }

        return orderedCards;
    }

    private async Task InsertGradesToDb(List<CardDto> cards)
    {
        foreach (var cardDto in cards)
        {
            var cardDal = _mapper.Map<CardDal>(cardDto);
            await _gradeRepository.InsertGradeAsync(cardDal.Grades!.Last());
        }

        await _gradeRepository.SaveAsync();
    }
}