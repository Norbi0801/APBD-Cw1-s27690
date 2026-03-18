using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ReturnDeviceCommand : ICommand
{
    private readonly RentalService _rentalService;

    public string Name => "ReturnDevice";

    public ReturnDeviceCommand(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public void Execute()
    {
        Display.Header("Return Device");

        var activeRentals = _rentalService.GetAllRentals().Where(r => !r.IsReturned()).ToList();
        var rental = Input.Select("Select rental to return", activeRentals,
            r => $"{r.Renter.Name} {r.Renter.Surname} -> {r.Device.Name} (due {r.DueDateTime:d})");

        var returned = _rentalService.ReturnDevice(rental.Uuid);
        Display.Success($"Returned. Fee: {returned.ReturnFee}, Late fee: {returned.LateFee}");
    }
}
