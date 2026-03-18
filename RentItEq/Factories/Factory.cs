using RentItEq.DTO;
using RentItEq.Models.Devices;

namespace RentItEq.Factories;

public static class DeviceFactory
{
    public static Device Create(DeviceDto dto) => dto switch
    {
        LaptopDto l => new Laptop
        {
            Name = l.Name,
            SerialNumber = l.SerialNumber,
            Model = l.Model,
            Manufacturer = l.Manufacturer,
            RamSizeGb = l.RamSizeGb,
            ProcessorModel = l.ProcessorModel
        },
        ProjectorDto p => new Projector
        {
            Name = p.Name,
            SerialNumber = p.SerialNumber,
            Model = p.Model,
            Manufacturer = p.Manufacturer,
            Brightness = p.Brightness,
            NativeResolution = p.NativeResolution
        },
        CameraDto c => new Camera
        {
            Name = c.Name,
            SerialNumber = c.SerialNumber,
            Model = c.Model,
            Manufacturer = c.Manufacturer,
            LensType = c.LensType,
            SensorResolution = c.SensorResolution
        },
        _ => throw new ArgumentException($"Unknown device type: {dto.Type}")
    };
}
