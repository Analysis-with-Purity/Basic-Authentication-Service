using AuthMicroservice.Dtos;
using FluentValidation;

namespace Application.Validators;

public class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {


        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("EmailAddress cannot be empty")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .Length(6, 30).WithMessage("Password must be between 6 and 30 characters")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$")
            .WithMessage(
                "Password must contain at least one uppercase, one lowercase, one number, and one special character");

    }
}