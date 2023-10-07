using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
public record AddUserRoleCommand(Guid UserId, List<Role> RolesToAdd)
    : ICreateCommand<User>;
