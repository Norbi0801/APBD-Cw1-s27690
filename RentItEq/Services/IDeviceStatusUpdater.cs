using RentItEq.Models.Devices;
using RentItEq.Types;

namespace RentItEq.Services;

public interface IDeviceStatusUpdater
{
    Device GetDeviceOrThrow(Guid deviceUuid);
    void UpdateStatus(Guid deviceUuid, DeviceStatus status);
}
