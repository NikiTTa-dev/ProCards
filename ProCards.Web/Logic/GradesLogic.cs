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
        await _gradeRepository.InsertGradesAsync(cards
            .SelectMany(card =>
            {
                var cardDal = _mapper.Map<CardDal>(card);
                return cardDal.Grades;
            })
            .ToList());
        await _gradeRepository.SaveAsync();
        
        
        
        return null;
    }
}