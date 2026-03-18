using System.Text.Json.Serialization;
using RentItEq.Types;

namespace RentItEq.Models.Devices;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]                    
[JsonDerivedType(typeof(Laptop), "laptop")]               
[JsonDerivedType(typeof(Projector), "projector")]                             
[JsonDerivedType(typeof(Camera), "camera")]
public abstract class Device : BaseEntity
{
    public required String Name { get; set; }
    public String? SerialNumber { get; set; }
    public String? Model { get; set; }
    public String? Manufacturer { get; set; }
    public DeviceStatus Status { get; set; } = DeviceStatus.Available;
}
