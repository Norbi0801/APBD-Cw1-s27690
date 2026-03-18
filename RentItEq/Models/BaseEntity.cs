namespace RentItEq.Models;

public abstract class BaseEntity
{
    public Guid Uuid { get; init; } = Guid.NewGuid();
}