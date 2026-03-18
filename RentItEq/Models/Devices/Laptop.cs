namespace RentItEq.Models.Devices;

public class Laptop : Device
{
    public required double RamSizeGb { get; set; }
    public required string ProcessorModel { get; set; }
}
