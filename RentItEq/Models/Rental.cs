using RentItEq.Models.Devices;
using RentItEq.Models.Users;
using RentItEq.Types;

namespace RentItEq.Models;

public class Rental : BaseEntity
{
    public required User Renter { get; set; }
    public required Device Device { get; set; }
    public DateTime StartRentalDateTime { get; set; }
    public DateTime DueDateTime { get; set; }
    public DateTime? ReturnDateTime { get; set; }
    public Money ReturnFee { get; set; }
    public Money LateFee { get; set; }

    public bool IsReturned()
    {
        return ReturnDateTime.HasValue;
    }

    public bool IsTimelyReturned()
    {
        return IsReturned() && ReturnDateTime!.Value <= DueDateTime;
    }
}
