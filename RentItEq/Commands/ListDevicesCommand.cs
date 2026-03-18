using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListDevicesCommand : ICommand
{
    private readonly DeviceService _deviceService;

    public string Name => "ListDevices";

    public ListDevicesCommand(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public void Execute()
    {
        Display.Header("All Devices");
        Display.ListItems(
            _deviceService.GetDeviceList(),
            d => $"[{d.Uuid}] {d.Name} ({d.GetType().Name}) - {d.Status}",
            "No devices registered."
        );
    }
}
