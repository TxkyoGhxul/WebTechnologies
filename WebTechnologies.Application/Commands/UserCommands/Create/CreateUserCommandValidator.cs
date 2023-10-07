using FluentValidation;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
                .WithMessage("Provide a name")
            .NotEmpty()
                .WithMessage("Provide a name")
            .Length(5, 25)
                .WithMessage("Invalid length");

        RuleFor(c => c.Email)
            .NotNull()
                .WithMessage("Provide a email");

        RuleFor(c => c.BirthDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-100)))
                .WithMessage("You're not that old")
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-10)))
                .WithMessage("You're not that young");
    }
}
