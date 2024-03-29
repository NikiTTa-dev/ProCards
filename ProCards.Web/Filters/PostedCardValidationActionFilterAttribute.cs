﻿using System.Threading.Tasks;
using FluentValidation;
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
        var validator = new CardValidator(
            context.HttpContext.RequestServices.GetRequiredService<ICardRepository>());
        await validator.ValidateAsync(cardDto, options =>
        {
            options.ThrowOnFailures();
            options.IncludeRuleSets("PostCard");
        });


        await next.Invoke();
    }
}