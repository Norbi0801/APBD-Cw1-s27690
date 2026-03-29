using RentItEq.Components;
using RentItEq.DTO;
using RentItEq.Services;
using RentItEq.Types;

namespace RentItEq.Commands;

public class AddDeviceCommand : ICommand
{
    private readonly DeviceService _deviceService;
    private readonly Input _input;
    private readonly Display _display;

    public string Name => "AddDevice";

    public AddDeviceCommand(DeviceService deviceService, Input input, Display display)
    {
        _deviceService = deviceService;
        _input = input;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Add Device");

        var type = _input.ReadChoice("Device type", "laptop", "projector", "camera");
        var name = _input.ReadString("Name");
        var serial = _input.ReadOptional("Serial number");
        var model = _input.ReadOptional("Model");
        var manufacturer = _input.ReadOptional("Manufacturer");

        DeviceDto dto = type switch
        {
            "laptop" => CreateLaptopDto(name, serial, model, manufacturer),
            "projector" => CreateProjectorDto(name, serial, model, manufacturer),
            "camera" => CreateCameraDto(name, serial, model, manufacturer),
            _ => throw new ArgumentException($"Unknown device type: {type}")
        };

        _deviceService.AddDevice(dto);
        _display.Success($"Device '{name}' added.");
    }

    private LaptopDto CreateLaptopDto(string name, string? serial, string? model, string? manufacturer)
    {
        var ram = _input.ReadDouble("RAM (GB)");
        var processor = _input.ReadString("Processor model");
        return new LaptopDto(Guid.NewGuid(), name, serial, model, manufacturer, ram, processor);
    }

    private ProjectorDto CreateProjectorDto(string name, string? serial, string? model, string? manufacturer)
    {
        var brightness = _input.ReadInt("Brightness (lumens)");
        var width = _input.ReadInt("Resolution width");
        var height = _input.ReadInt("Resolution height");
        return new ProjectorDto(Guid.NewGuid(), name, serial, model, manufacturer, brightness, new Resolution(width, height));
    }

    private CameraDto CreateCameraDto(string name, string? serial, string? model, string? manufacturer)
    {
        var lens = _input.ReadString("Lens type");
        var sensor = _input.ReadDouble("Sensor resolution (MP)");
        return new CameraDto(Guid.NewGuid(), name, serial, model, manufacturer, lens, sensor);
    }
}
