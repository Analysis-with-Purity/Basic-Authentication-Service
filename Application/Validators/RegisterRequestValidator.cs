using AuthMicroservice.Dtos;
using FluentValidation;

namespace Application.Validators;

public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("EmailAddress cannot be empty")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty")
            .Length(2, 50).WithMessage("UserName must be between 2 and 50 characters");

        RuleFor(x => x.GenderId)
            .NotEmpty().WithMessage("GenderId cannot be empty")
            .GreaterThan(0).WithMessage("GenderId must be a valid value");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .Length(6, 30).WithMessage("Password must be between 6 and 30 characters")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$")
            .WithMessage(
                "Password must contain at least one uppercase, one lowercase, one number, and one special character");

        RuleFor(x => x.PhoneNo)
            .NotEmpty().WithMessage("PhoneNo cannot be empty")
            .Length(11).WithMessage("Phone number must be exactly 11 digits");

        RuleFor(x => x.DOB)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateTime.Today)
            .WithMessage("Date of birth must be in the past");
    }
}