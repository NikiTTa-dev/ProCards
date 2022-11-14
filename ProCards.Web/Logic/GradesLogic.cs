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
    private IMapper _mapper;

    public GradesLogic(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
    }

    public async Task<List<CardDto>> GetNewCardsByGrades(List<CardDto> cards)
    {
        await InsertGradesToDb(cards);

        

        return null;
    }

    private async Task InsertGradesToDb(List<CardDto> cards)
    {
        foreach (var cardDto in cards)
        {
            var cardDal = _mapper.Map<CardDal>(cardDto);
            if (cardDal.Grades == null || cardDal.Grades.Count == 0)
                throw new ArgumentException("Card have no grades.");
            await _gradeRepository.InsertGradesAsync(cardDal.Grades.ToList());
        }

        await _gradeRepository.SaveAsync();
    }
}