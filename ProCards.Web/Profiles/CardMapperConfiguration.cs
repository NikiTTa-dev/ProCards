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
            .ForMember(d => d.CardCategory,
                opt => opt.MapFrom(s => s.Category))
            .ForMember(d => d.Grades,
                opt => opt.Ignore());
        CreateMap<CardDto, CardDal>()
            .ForMember(d => d.Category,
                opt => opt.MapFrom(s => s.CardCategory))
            .ForMember(d => d.PublishedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(d => d.Grades,
                opt => opt.MapFrom(s => s.Grades
                    .Select(g => new GradeDal
                    {
                        CardId = s.FirstSide,
                        GradeNumber = g,
                        PublishedAt = DateTime.UtcNow
                    })));
    }
}