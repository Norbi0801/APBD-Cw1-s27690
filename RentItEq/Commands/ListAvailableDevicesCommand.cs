using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListAvailableDevicesCommand : ICommand
{
    private readonly DeviceService _deviceService;
    private readonly Display _display;

    public string Name => "ListAvailableDevices";

    public ListAvailableDevicesCommand(DeviceService deviceService, Display display)
    {
        _deviceService = deviceService;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Available Devices");
        _display.ListItems(
            _deviceService.GetAvailableDevices(),
            d => $"[{d.Uuid}] {d.Name} ({d.GetType().Name})",
            "No available devices."
        );
    }
}
