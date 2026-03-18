using RentItEq.Components;
using RentItEq.Models.Devices;
using RentItEq.Services;
using RentItEq.Types;

namespace RentItEq.Commands;

public class UpdateDeviceStatusCommand : ICommand
{
    private readonly DeviceService _deviceService;

    public string Name => "UpdateDeviceStatus";

    public UpdateDeviceStatusCommand(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public void Execute()
    {
        Display.Header("Update Device Status");

        var device = Input.Select("Select device", _deviceService.GetDeviceList(),
            d => $"{d.Name} ({d.GetType().Name}) - {d.Status}");

        var status = Input.ReadEnum<DeviceStatus>("New status");

        _deviceService.UpdateStatus(device.Uuid, status);
        Display.Success($"{device.Name} status changed to {status}.");
    }
}
