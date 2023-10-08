namespace WebTechnologies.Domain.Exceptions;
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base("Entity not found")
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(Guid id) : base($"Entity with ID: {id} not found")
    {
        Id = id;
    }

    public EntityNotFoundException(Guid id, Type entityType) : base($"Entity {entityType.Name} with ID: {id} not found")
    {
        Id = id;
        EntityType = entityType;
    }

    public Guid Id { get; init; }

    public Type EntityType { get; init; }
}
