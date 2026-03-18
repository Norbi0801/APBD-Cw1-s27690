using RentItEq.Data;
using RentItEq.DTO;
using RentItEq.Exceptions;
using RentItEq.Factories;
using RentItEq.Models.Devices;
using RentItEq.Types;

namespace RentItEq.Services;

public class DeviceService : BaseService<Device>, IDeviceStatusUpdater
{
    private readonly IRentalChecker _rentalChecker;

    public DeviceService(IRepository<Device> repo, IRentalChecker rentalChecker) : base(repo)
    {
        _rentalChecker = rentalChecker;
    }

    public List<Device> GetDeviceList()
    {
        return Repo.GetAll();
    }

    public void AddDevice(DeviceDto deviceDto)
    {
        var device = DeviceFactory.Create(deviceDto);
        Repo.Add(device);
    }

    public List<Device> GetAvailableDevices()
    {
        return Repo.GetAll().Where(d => d.Status == DeviceStatus.Available).ToList();
    }

    public Device GetDeviceOrThrow(Guid deviceUuid)
    {
        return Repo.GetById(deviceUuid)
               ?? throw new KeyNotFoundException($"Device with id {deviceUuid} not found");
    }

    public void UpdateStatus(Guid deviceUuid, DeviceStatus status)
    {
        var device = GetDeviceOrThrow(deviceUuid);

        if (_rentalChecker.HasDeviceActiveRental(deviceUuid))
        {
            throw new DeviceUnavailableException(device.Name, "currently rented");
        }

        device.Status = status;
        Repo.SaveChanges();
    }
}

