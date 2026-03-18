using RentItEq.Data;
using RentItEq.Exceptions;
using RentItEq.Models;
using RentItEq.Models.Users;
using RentItEq.Types;

namespace RentItEq.Services;

public class RentalService : BaseService<Rental>, IRentalChecker
{
    private IDeviceStatusUpdater? _deviceStatusUpdater;
    private readonly RentalPolicy _policy;
    private readonly IUserProvider _userProvider;

    private IDeviceStatusUpdater DeviceStatusUpdater =>
        _deviceStatusUpdater ?? throw new InvalidOperationException(
            "DeviceStatusUpdater not configured. Call SetDeviceStatusUpdater first.");

    public RentalService(IRepository<Rental> repo, RentalPolicy policy, IUserProvider userProvider) : base(repo)
    {
        _policy = policy;
        _userProvider = userProvider;
    }

    public void SetDeviceStatusUpdater(IDeviceStatusUpdater updater)
    {
        _deviceStatusUpdater = updater;
    }

    public Rental RentDevice(Guid renterUuid, Guid deviceUuid, int rentalDays = 14)
    {
        var renter = _userProvider.GetUserOrThrow(renterUuid);
        var device = DeviceStatusUpdater.GetDeviceOrThrow(deviceUuid);

        if (device.Status != DeviceStatus.Available)
            throw new DeviceUnavailableException(device.Name, device.Status.ToString());

        var activeCount = GetActiveUserRentals(renter.Uuid).Count;
        var limit = _policy.GetRentalLimit(renter.Type);
        if (activeCount >= limit)
            throw new RentalLimitException($"{renter.Name} {renter.Surname}", activeCount, limit);

        DeviceStatusUpdater.UpdateStatus(deviceUuid, DeviceStatus.Rented);

        var rental = new Rental
        {
            Renter = renter,
            Device = device,
            StartRentalDateTime = DateTime.Now,
            DueDateTime = DateTime.Now.AddDays(rentalDays)
        };

        Repo.Add(rental);

        return rental;
    }

    public Rental ReturnDevice(Guid rentalUuid)
    {
        var rental = Repo.GetById(rentalUuid)
                     ?? throw new KeyNotFoundException($"Rental with id {rentalUuid} not found");

        if (rental.IsReturned())
            throw new InvalidOperationException("Rental already returned");

        rental.ReturnDateTime = DateTime.Now;

        var totalDays = (rental.ReturnDateTime - rental.StartRentalDateTime).Days;
        rental.ReturnFee = new Money(totalDays * _policy.BaseFeePerDay);

        if (!rental.IsTimelyReturned())
        {
            var overdueDays = (rental.ReturnDateTime - rental.DueDateTime).Days;
            rental.LateFee = new Money(overdueDays * _policy.LateFeePerDay);
        }

        DeviceStatusUpdater.UpdateStatus(rental.Device.Uuid, DeviceStatus.Available);
        Repo.SaveChanges();

        return rental;
    }

    public List<Rental> GetAllRentals()
    {
        return Repo.GetAll();
    }

    public List<Rental> GetActiveUserRentals(Guid userUuid)
    {
        return Repo.GetAll().Where(r => r.Renter.Uuid == userUuid && !r.IsReturned()).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return Repo.GetAll().Where(r => !r.IsReturned() && r.DueDateTime < DateTime.Now).ToList();
    }

    public Rental? GetActiveRentalByDeviceId(Guid uuid)
    {
        return Repo.GetAll().ToList().Find(x => x.Device.Uuid == uuid && !x.IsReturned());
    }

    public bool HasDeviceActiveRental(Guid deviceUuid)
    {
        return GetActiveRentalByDeviceId(deviceUuid) != null;
    }
}
