using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ReturnDeviceCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly Input _input;
    private readonly Display _display;

    public string Name => "ReturnDevice";

    public ReturnDeviceCommand(RentalService rentalService, Input input, Display display)
    {
        _rentalService = rentalService;
        _input = input;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Return Device");

        var activeRentals = _rentalService.GetAllRentals().Where(r => !r.IsReturned()).ToList();
        var rental = _input.Select("Select rental to return", activeRentals,
            r => $"{r.Renter.Name} {r.Renter.Surname} -> {r.Device.Name} (due {r.DueDateTime:d})");

        var returned = _rentalService.ReturnDevice(rental.Uuid);
        _display.Success($"Returned. Fee: {returned.ReturnFee}, Late fee: {returned.LateFee}");
    }
}
