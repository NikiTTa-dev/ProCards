﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProCards.DAL.Models;

namespace ProCards.DAL.Context.Configurations;

public class GradeConfiguration: IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.Property(p => p.GradeNumber)
            .IsRequired();
        builder.Navigation(p => p.Card)
            .IsRequired();
    }
}