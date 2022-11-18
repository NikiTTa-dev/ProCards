using System;
using System.Collections.Generic;
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
                opt => opt.MapFrom(grades => new List<int>()));
        CreateMap<CardDto, CardDal>()
            .ForMember(d => d.PublishedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(d => d.Grades,
                opt =>
                {
                    opt.AllowNull();
                    opt.MapFrom((s, d) => s.Grades?.Select(grade => new GradeDal
                        {
                            CardId = s.Id,
                            GradeNumber = grade,
                            PublishedAt = DateTime.UtcNow
                        })
                    );
                });
    }
}