using RentItEq.Types;

namespace RentItEq.Services;

public class RentalPolicy
{
    public readonly decimal BaseFeePerDay = 10m;
    public readonly decimal LateFeePerDay = 25m;

    public Dictionary<UserType, int> RentalLimits { get; } = new()
    {
        { UserType.Student, 2 },
        { UserType.Employee, 5 }
    };

    public int GetRentalLimit(UserType type)
    {
        return RentalLimits.GetValueOrDefault(type, 0);
    }
}
