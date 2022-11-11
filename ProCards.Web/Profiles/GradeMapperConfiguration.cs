using System;
using AutoMapper;
using ProCards.DAL.Models;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Profiles;

public class GradeMapperConfiguration: Profile
{
    public GradeMapperConfiguration()
    {
        CreateMap<GradeDal, GradeDto>();
        CreateMap<GradeDto, GradeDal>()
            .ForMember(d=>d.PublishedAt,
                opt=>opt.MapFrom(src=>DateTime.UtcNow));
    }
}