using WebTechnologies.Application.Interfaces;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
public record GenerateTokenCommand(Guid UserId) : ICreateCommand<string>;
