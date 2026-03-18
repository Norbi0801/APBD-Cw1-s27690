namespace RentItEq.Exceptions;

public class RentalLimitException : Exception
{
    public RentalLimitException(string userName, int activeCount, int limit)
        : base($"User '{userName}' has {activeCount}/{limit} active rentals. Limit reached.") { }
}
