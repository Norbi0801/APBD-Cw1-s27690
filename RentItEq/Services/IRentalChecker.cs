namespace RentItEq.Services;

public interface IRentalChecker
{
    bool HasDeviceActiveRental(Guid deviceUuid);
}
