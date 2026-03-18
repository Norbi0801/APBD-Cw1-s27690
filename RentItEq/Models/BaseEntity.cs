namespace RentItEq.Models;

public abstract class BaseEntity
{
    public Guid Uuid { get; } = Guid.NewGuid();
}