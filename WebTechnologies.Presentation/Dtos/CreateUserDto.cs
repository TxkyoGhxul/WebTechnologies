namespace WebTechnologies.Presentation.Dtos;
public record CreateUserDto(string Name, string Email, DateOnly BirthDate);
public record DeleteUserDto(Guid UserId);
public record UpdateUserDto(Guid UserId, string Name, string Email, DateOnly BirthDate);
public record AddUserRoleDto(Guid UserId, Guid RoleId);
