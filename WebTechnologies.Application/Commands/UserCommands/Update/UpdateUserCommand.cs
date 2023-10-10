using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Update;
public record UpdateUserCommand(Guid UserId, string Name, string Email, DateOnly BirthDate)
    : IUpdateCommand<User>;