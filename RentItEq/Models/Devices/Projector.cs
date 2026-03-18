using RentItEq.Types;

namespace RentItEq.Models.Devices;

public class Projector : Device
{
    public int Brightness { get; set; }
    public Resolution NativeResolution { get; set; }
}
