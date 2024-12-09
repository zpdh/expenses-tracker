using ExpensesTracker.Domain.Errors.Base;
using FluentValidation;

namespace ExpensesTracker.Application.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilderOptions, Error error)
    {
        return ruleBuilderOptions.WithErrorCode(nameof(error)).WithMessage(error.ErrorMessage);
    }
}