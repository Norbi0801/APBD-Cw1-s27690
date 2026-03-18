namespace RentItEq.Models.Devices;

public class Camera : Device
{
    public required string LensType { get; set; }
    public required double SensorResolution { get; set; }
}
