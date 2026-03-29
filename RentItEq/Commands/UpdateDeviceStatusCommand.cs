using RentItEq.Components;
using RentItEq.Services;
using RentItEq.Types;

namespace RentItEq.Commands;

public class UpdateDeviceStatusCommand : ICommand
{
    private readonly DeviceService _deviceService;
    private readonly Input _input;
    private readonly Display _display;

    public string Name => "UpdateDeviceStatus";

    public UpdateDeviceStatusCommand(DeviceService deviceService, Input input, Display display)
    {
        _deviceService = deviceService;
        _input = input;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Update Device Status");

        var device = _input.Select("Select device", _deviceService.GetDeviceList(),
            d => $"{d.Name} ({d.GetType().Name}) - {d.Status}");

        var status = _input.ReadEnum<DeviceStatus>("New status");

        _deviceService.UpdateStatus(device.Uuid, status);
        _display.Success($"{device.Name} status changed to {status}.");
    }
}
