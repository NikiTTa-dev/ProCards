using System;
using AutoMapper;
using ProCards.DAL.Models;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Profiles;

public class CategoryMapperConfiguration: Profile
{
    public CategoryMapperConfiguration()
    {
        CreateMap<CategoryDal, CategoryDto>();
        CreateMap<CategoryDto, CategoryDal>()
            .ForMember(d=>d.PublishedAt,
                opt=>opt.MapFrom(src=>DateTime.UtcNow));
    }
}