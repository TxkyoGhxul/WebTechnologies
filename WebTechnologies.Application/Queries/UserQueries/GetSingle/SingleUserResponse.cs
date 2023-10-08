using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Queries.UserQueries.GetSingle;
public record SingleUserResponse(Guid Id, DateOnly BirthDate, int Age, string Name, string Email, List<Role> Roles);
