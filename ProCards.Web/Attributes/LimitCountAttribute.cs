using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ProCards.Web.Attributes;

public class LimitCountAttribute : ValidationAttribute
{
    private readonly int _min;
    private readonly int _max;

    public LimitCountAttribute(int min, int max)
    {
        _min = min;
        _max = max;
    }

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        var list = value as IList;
        if (list == null)
            return new ValidationResult($"{context.MemberName} is null",
                new[] { context.MemberName });

        if (list.Count < _min || list.Count > _max)
            return new ValidationResult($"{context.MemberName} count must be between {_min} and {_max}",
                new[] { context.MemberName });

        return ValidationResult.Success;
    }
}