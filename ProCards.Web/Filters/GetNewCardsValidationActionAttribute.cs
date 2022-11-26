using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ProCards.DAL.Interfaces;
using ProCards.Web.Data.DTOs;
using ProCards.Web.Validators;

namespace ProCards.Web.Filters;

public class GetNewCardsValidationActionAttribute: ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cardDto = (List<CardDto>)context.ActionArguments["cardDtos"];
        var validator = new CardValidator(
            context.HttpContext.RequestServices.GetRequiredService<ICardRepository>());
        foreach (var card in cardDto)
        {
            await validator.ValidateAsync(card, options =>
            {
                options.ThrowOnFailures();
                options.IncludeRuleSets("GetCard");
            }); 
        }
        
        await next.Invoke();
    }
}