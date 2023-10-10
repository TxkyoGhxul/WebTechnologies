using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Delete;
public record DeleteUserCommand(Guid UserId) : IDeleteCommand<User>;