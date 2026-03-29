using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ReportCommand : ICommand
{
    private readonly DeviceService _deviceService;
    private readonly UserService _userService;
    private readonly RentalService _rentalService;
    private readonly Display _display;

    public string Name => "Report";

    public ReportCommand(DeviceService deviceService, UserService userService, RentalService rentalService, Display display)
    {
        _deviceService = deviceService;
        _userService = userService;
        _rentalService = rentalService;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Rental System Report");

        var allDevices = _deviceService.GetDeviceList();
        var availableDevices = _deviceService.GetAvailableDevices();
        var allRentals = _rentalService.GetAllRentals();
        var activeRentals = allRentals.Where(r => !r.IsReturned()).ToList();
        var overdueRentals = _rentalService.GetOverdueRentals();
        var users = _userService.GetUserList();
        var totalFees = allRentals.Sum(r => r.ReturnFee.Amount + r.LateFee.Amount);

        _display.Info($"Devices:          {allDevices.Count} total, {availableDevices.Count} available");
        _display.Info($"Users:            {users.Count}");
        _display.Info($"Rentals:          {allRentals.Count} total, {activeRentals.Count} active");
        _display.Info($"Overdue:          {overdueRentals.Count}");
        _display.Info($"Total fees:       {totalFees:N2} PLN");
    }
}
