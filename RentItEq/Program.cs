using RentItEq.Commands;
using RentItEq.Data;
using RentItEq.Models;
using RentItEq.Models.Devices;
using RentItEq.Models.Users;
using RentItEq.Services;
using RentItEq.UI;

var deviceRepo = new JsonRepository<Device>("devices.json");
var userRepo = new JsonRepository<User>("users.json");
var rentalRepo = new JsonRepository<Rental>("rentals.json");

var userService = new UserService(userRepo);

var rentalPolicy = new RentalPolicy();
var rentalService = new RentalService(rentalRepo, rentalPolicy, userService);
var deviceService = new DeviceService(deviceRepo, rentalService);
rentalService.SetDeviceStatusUpdater(deviceService);

var commands = new List<ICommand>
{
    new AddDeviceCommand(deviceService),
    new ListDevicesCommand(deviceService),
    new ListAvailableDevicesCommand(deviceService),
    new UpdateDeviceStatusCommand(deviceService),
    new RegisterUserCommand(userService),
    new RentDeviceCommand(rentalService, userService, deviceService),
    new ReturnDeviceCommand(rentalService),
    new ListRentalsCommand(rentalService),
    new ListUserRentalsCommand(rentalService, userService),
    new ListOverdueRentalsCommand(rentalService),
    new ReportCommand(deviceService, userService, rentalService),
    new ExitCommand()
};

var app = new App(commands);
app.Run();
