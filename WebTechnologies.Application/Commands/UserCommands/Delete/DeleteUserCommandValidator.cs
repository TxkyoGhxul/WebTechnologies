using FluentValidation;

namespace WebTechnologies.Application.Commands.UserCommands.Delete;
internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}
