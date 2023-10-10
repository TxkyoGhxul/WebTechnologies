using FluentValidation;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
internal class AddUserRoleCommandValidator : AbstractValidator<AddUserRoleCommand>
{
    public AddUserRoleCommandValidator()
    {
        RuleFor(c =>  c.UserId)
            .NotEmpty()
                .WithMessage("Prodive a user");

        RuleFor(c => c.RoleId)
            .NotNull()
            .NotEmpty()
                .WithMessage("No roles to add");
    }
}
