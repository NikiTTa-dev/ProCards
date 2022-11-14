using System;
using AutoMapper;
using ProCards.DAL.Models;
using ProCards.Web.Data.DTOs;
using System.Linq;

namespace ProCards.Web.Profiles;

public class CardMapperConfiguration : Profile
{
    public CardMapperConfiguration()
    {
        CreateMap<CardDal, CardDto>()
            .ForMember(d => d.Grades,
                opt => opt.Ignore());
        CreateMap<CardDto, CardDal>()
            .ForMember(d => d.PublishedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(d => d.Grades,
                opt => opt.MapFrom(s => s.Grades
                    .Select(grade => new GradeDal
                    { 
                        CardId = s.Id,
                        GradeNumber = grade,
                        PublishedAt = DateTime.UtcNow
                    })));
    }
}