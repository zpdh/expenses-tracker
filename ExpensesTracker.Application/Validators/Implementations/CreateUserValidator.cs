using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Extensions;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Results;

// ReSharper disable ConvertIfStatementToReturnStatement

namespace ExpensesTracker.Application.Validators.Implementations;

public class CreateUserValidator : IValidator<CreateUserRequest>
{
    public Result Validate(CreateUserRequest request)
    {
        if (request.Name.Length is < 4 or > 36)
        {
            return UserError.UsernameLength;
        }

        if (!request.Email.IsValidEmail())
        {
            return UserError.InvalidEmail;
        }

        if (request.Password.Length is < 6 or > 32)
        {
            return UserError.PasswordLength;
        }

        return Result.Success();
    }
}