using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ReportCommand : ICommand
{
    private readonly DeviceService _deviceService;
    private readonly UserService _userService;
    private readonly RentalService _rentalService;

    public string Name => "Report";

    public ReportCommand(DeviceService deviceService, UserService userService, RentalService rentalService)
    {
        _deviceService = deviceService;
        _userService = userService;
        _rentalService = rentalService;
    }

    public void Execute()
    {
        Display.Header("Rental System Report");

        var allDevices = _deviceService.GetDeviceList();
        var availableDevices = _deviceService.GetAvailableDevices();
        var allRentals = _rentalService.GetAllRentals();
        var activeRentals = allRentals.Where(r => !r.IsReturned()).ToList();
        var overdueRentals = _rentalService.GetOverdueRentals();
        var users = _userService.GetUserList();
        var totalFees = allRentals.Sum(r => r.ReturnFee.Amount + r.LateFee.Amount);

        Console.WriteLine($"  Devices:          {allDevices.Count} total, {availableDevices.Count} available");
        Console.WriteLine($"  Users:            {users.Count}");
        Console.WriteLine($"  Rentals:          {allRentals.Count} total, {activeRentals.Count} active");
        Console.WriteLine($"  Overdue:          {overdueRentals.Count}");
        Console.WriteLine($"  Total fees:       {totalFees:N2} PLN");
    }
}
