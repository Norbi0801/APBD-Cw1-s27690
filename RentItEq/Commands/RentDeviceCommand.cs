using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class RentDeviceCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly UserService _userService;
    private readonly DeviceService _deviceService;

    public string Name => "RentDevice";

    public RentDeviceCommand(RentalService rentalService, UserService userService, DeviceService deviceService)
    {
        _rentalService = rentalService;
        _userService = userService;
        _deviceService = deviceService;
    }

    public void Execute()
    {
        Display.Header("Rent Device");

        var users = _userService.GetUserList();
        var user = Input.Select("Select user", users, u => $"{u.Name} {u.Surname} ({u.Type})");

        var devices = _deviceService.GetAvailableDevices();
        var device = Input.Select("Select device", devices, d => $"{d.Name} ({d.GetType().Name}) - {d.Status}");

        var days = Input.ReadInt("Rental days", 14);

        var rental = _rentalService.RentDevice(user.Uuid, device.Uuid, days);
        Display.Success($"Rental ID: {rental.Uuid}, due: {rental.DueDateTime:d}");
    }
}
