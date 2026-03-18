using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListAvailableDevicesCommand : ICommand
{
    private readonly DeviceService _deviceService;

    public string Name => "ListAvailableDevices";

    public ListAvailableDevicesCommand(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public void Execute()
    {
        Display.Header("Available Devices");
        Display.ListItems(
            _deviceService.GetAvailableDevices(),
            d => $"[{d.Uuid}] {d.Name} ({d.GetType().Name})",
            "No available devices."
        );
    }
}
