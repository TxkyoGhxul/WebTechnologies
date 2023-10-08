using FluentValidation;

namespace WebTechnologies.Application.Commands.UserCommands.Create;

internal class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
{
    public GenerateTokenCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
                .WithMessage("Prodive a user");
    }
}
