using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class RentDeviceCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly UserService _userService;
    private readonly DeviceService _deviceService;
    private readonly Input _input;
    private readonly Display _display;

    public string Name => "RentDevice";

    public RentDeviceCommand(RentalService rentalService, UserService userService, DeviceService deviceService, Input input, Display display)
    {
        _rentalService = rentalService;
        _userService = userService;
        _deviceService = deviceService;
        _input = input;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Rent Device");

        var users = _userService.GetUserList();
        var user = _input.Select("Select user", users, u => $"{u.Name} {u.Surname} ({u.Type})");

        var devices = _deviceService.GetAvailableDevices();
        var device = _input.Select("Select device", devices, d => $"{d.Name} ({d.GetType().Name}) - {d.Status}");

        var days = _input.ReadInt("Rental days", 14);

        var rental = _rentalService.RentDevice(user.Uuid, device.Uuid, days);
        _display.Success($"Rental ID: {rental.Uuid}, due: {rental.DueDateTime:d}");
    }
}
