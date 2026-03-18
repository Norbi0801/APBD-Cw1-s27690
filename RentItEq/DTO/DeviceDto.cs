using RentItEq.Types;

namespace RentItEq.DTO;

public record DeviceDto(
    Guid Uuid,
    string Type,
    string Name,
    string? SerialNumber,
    string? Model,
    string? Manufacturer
);

public record LaptopDto(
    Guid Uuid,
    string Name,
    string? SerialNumber,
    string? Model,
    string? Manufacturer,
    double RamSizeGb,
    string ProcessorModel
) : DeviceDto(Uuid, "laptop", Name, SerialNumber, Model, Manufacturer);

public record ProjectorDto(
    Guid Uuid,
    string Name,
    string? SerialNumber,
    string? Model,
    string? Manufacturer,
    int Brightness,
    Resolution NativeResolution
) : DeviceDto(Uuid, "projector", Name, SerialNumber, Model, Manufacturer);

public record CameraDto(
    Guid Uuid,
    string Name,
    string? SerialNumber,
    string? Model,
    string? Manufacturer,
    string LensType,
    double SensorResolution
) : DeviceDto(Uuid, "camera", Name, SerialNumber, Model, Manufacturer);
