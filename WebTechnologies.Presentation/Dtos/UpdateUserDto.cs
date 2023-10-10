namespace WebTechnologies.Presentation.Dtos;

public record UpdateUserDto(Guid UserId, string Name, string Email, DateOnly BirthDate);
