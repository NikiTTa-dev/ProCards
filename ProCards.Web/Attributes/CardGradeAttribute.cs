using System;
using System.ComponentModel.DataAnnotations;
using ProCards.Web.Data.DTOs;

namespace ProCards.Web.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CardGradeAttribute: ValidationAttribute
{
    private CardDto _dto;
    
    public override bool IsValid(object value)
    {
        if (!(value is CardDto cardDto))
            throw new ValidationException($"The required attribute must be of type CardDto");
        
        if (cardDto.Grade == null)
            return true;
        
        _dto = cardDto;

        if (cardDto.FirstSide != cardDto.Grade.CardFirstSide)
            return false;
        
        return true;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"Property {nameof(_dto.Grade.CardFirstSide)} must have the same value as {nameof(_dto.FirstSide)}";
    }
}