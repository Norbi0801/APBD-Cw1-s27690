using RentItEq.Components;
using RentItEq.DTO;
using RentItEq.Services;
using RentItEq.Types;

namespace RentItEq.Commands;

public class AddDeviceCommand : ICommand
{
    private readonly DeviceService _deviceService;

    public string Name => "AddDevice";

    public AddDeviceCommand(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public void Execute()
    {
        Display.Header("Add Device");

        var type = Input.ReadChoice("Device type", "laptop", "projector", "camera");
        var name = Input.ReadString("Name");
        var serial = Input.ReadOptional("Serial number");
        var model = Input.ReadOptional("Model");
        var manufacturer = Input.ReadOptional("Manufacturer");

        DeviceDto dto = type switch
        {
            "laptop" => CreateLaptopDto(name, serial, model, manufacturer),
            "projector" => CreateProjectorDto(name, serial, model, manufacturer),
            "camera" => CreateCameraDto(name, serial, model, manufacturer),
            _ => throw new ArgumentException($"Unknown device type: {type}")
        };

        _deviceService.AddDevice(dto);
        Display.Success($"Device '{name}' added.");
    }

    private static LaptopDto CreateLaptopDto(string name, string? serial, string? model, string? manufacturer)
    {
        var ram = Input.ReadDouble("RAM (GB)");
        var processor = Input.ReadString("Processor model");
        return new LaptopDto(Guid.NewGuid(), name, serial, model, manufacturer, ram, processor);
    }

    private static ProjectorDto CreateProjectorDto(string name, string? serial, string? model, string? manufacturer)
    {
        var brightness = Input.ReadInt("Brightness (lumens)");
        var width = Input.ReadInt("Resolution width");
        var height = Input.ReadInt("Resolution height");
        return new ProjectorDto(Guid.NewGuid(), name, serial, model, manufacturer, brightness, new Resolution(width, height));
    }

    private static CameraDto CreateCameraDto(string name, string? serial, string? model, string? manufacturer)
    {
        var lens = Input.ReadString("Lens type");
        var sensor = Input.ReadDouble("Sensor resolution (MP)");
        return new CameraDto(Guid.NewGuid(), name, serial, model, manufacturer, lens, sensor);
    }
}
