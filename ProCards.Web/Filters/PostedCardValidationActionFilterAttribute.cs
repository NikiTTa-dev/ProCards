using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ProCards.DAL.Interfaces;
using ProCards.Web.Data.DTOs;
using ProCards.Web.Validators;

namespace ProCards.Web.Filters;

public class PostedCardValidationActionFilterAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cardDto = (CardDto)context.ActionArguments["card"];
        var repository = context.HttpContext.RequestServices.GetRequiredService<ICardRepository>();
        var validator = new CardValidator(repository);
        await validator.ValidateAsync(cardDto, options =>
        {
            options.ThrowOnFailures();
            options.IncludeRuleSets("asd");
        });

        // var cardDto = (CardDto)context.ActionArguments["card"];
        // if (cardDto.Category == null)
        //     throw new ArgumentException("Category field is required");
        //
        // if (cardDto.Category.IsUserCategory == false)
        //     throw new ArgumentException("You can insert only user categories.");
        //
        // if (cardDto.Id != null || cardDto.Category.Id != null)
        //     throw new ArgumentException("Card must have no ids when inserting to db.");
        //
        // if (cardDto.Grades != null && cardDto.Grades.Count > 0)
        //     throw new ArgumentException("Card must have no grades when inserting to db.");


        await next.Invoke();
    }
}