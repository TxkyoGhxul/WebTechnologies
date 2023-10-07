using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
public record CreateUserCommand(string Name, string Email, DateOnly BirthDate) 
    : ICreateCommand<User>;
