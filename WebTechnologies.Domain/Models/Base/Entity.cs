namespace WebTechnologies.Domain.Models.Base;

public abstract class Entity : IEntity
{
    public Guid Id { get; init; }
}
