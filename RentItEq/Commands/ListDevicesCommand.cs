using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListDevicesCommand : ICommand
{
    private readonly DeviceService _deviceService;
    private readonly Display _display;

    public string Name => "ListDevices";

    public ListDevicesCommand(DeviceService deviceService, Display display)
    {
        _deviceService = deviceService;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("All Devices");
        _display.ListItems(
            _deviceService.GetDeviceList(),
            d => $"[{d.Uuid}] {d.Name} ({d.GetType().Name}) - {d.Status}",
            "No devices registered."
        );
    }
}
