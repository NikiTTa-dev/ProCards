using System;
using System.Collections.Generic;
using ProCards.DAL.Context;
using ProCards.DAL.Models;

namespace ProCards.Web;

public static class SeedData
{
    private const int CardsCount = 100;
    private const int CategoriesCount = 30;
    private const int GradesCount = 10;

    private static List<string> _categories = new List<string>
    {
        "алгебра",
        "английский",
        "география",
        "информатика",
        "русский",
        "физика",
        "история",
        "биология",
        "химия",
        "обж"
    };

    public static void SeedDbData(AppDbContext context)
    {
        var categories = SeedCategories(context);
        var cards = SeedCards(context, categories);
        SeedGrades(context, cards);
    }

    private static List<CategoryDal> SeedCategories(AppDbContext context)
    {
        List<CategoryDal> categories = new List<CategoryDal>();
        bool categoryFlag = false;
        for (int i = 0; i < CategoriesCount; i++)
        {
            categories.Add(new CategoryDal
            {
                Name = categoryFlag ? i + 1 + "cat" : _categories[i],
                IsUserCategory = categoryFlag,
                PublishedAt = DateTime.UtcNow
            });
            if (i >= 9)
                categoryFlag = true;
        }

        context.AddRange(categories);
        context.SaveChanges();

        return categories;
    }

    private static List<CardDal> SeedCards(AppDbContext context, List<CategoryDal> categories)
    {
        List<CardDal> cards = new List<CardDal>();
        var rnd = new Random();
        for (int i = 0; i < categories.Count; i++)
        {
            for (int j = 0; j < rnd.Next(3, 20); j++)
                cards.Add(new CardDal
                {
                    Category = categories[i],
                    FirstSide = $"{categories[i].Name}" + (j + 1) + "card1",
                    SecondSide = $"{categories[i].Name}" + (j + 1) + "card2",
                    PublishedAt = DateTime.UtcNow
                });
        }

        context.AddRange(cards);
        context.SaveChanges();

        return cards;
    }

    private static void SeedGrades(AppDbContext context, List<CardDal> cards)
    {
        List<GradeDal> grades = new List<GradeDal>();
        var rnd = new Random();
        for (int i = 0; i < GradesCount; i++)
        {
            grades.Add(new GradeDal
            {
                Card = cards[rnd.Next(0, CardsCount)],
                GradeNumber = rnd.Next(1, 6),
                PublishedAt = DateTime.UtcNow
            });
        }

        context.AddRange(grades);
        context.SaveChanges();
    }
}