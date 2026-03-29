using RentItEq.Commands;
using RentItEq.Components;
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

Console.WriteLine("Select mode:");
Console.WriteLine("  1. Interactive");
Console.WriteLine("  2. Demo (scripted)");
Console.Write("> ");
var mode = Console.ReadLine()?.Trim();

IConsole console = mode == "2"
    ? new ScriptedConsole(
    [
        // --- Add 4 devices ---
        "1", "1", "Dell XPS 15", "SN-001", "XPS 15 9530", "Dell", "32", "Intel i7-13700H",
        "1", "2", "Epson EB-W51", "SN-002", "EB-W51", "Epson", "4000", "1920", "1080",
        "1", "3", "Canon EOS R6", "SN-003", "EOS R6 II", "Canon", "RF 24-105mm", "24",
        "1", "1", "MacBook Pro 16", "SN-004", "M3 Pro", "Apple", "36", "Apple M3 Pro",

        // --- Register 2 users ---
        "5", "Jan", "Kowalski", "2",
        "5", "Anna", "Nowak", "1",

        // --- 3 successful rentals ---
        "6", "1", "1", "14",
        "6", "1", "1", "-13",
        "6", "2", "1", "10",

        // --- Invalid: rental limit exceeded ---
        "6", "1", "1", "14",

        // --- Invalid: status change on rented device ---
        "4", "1", "4",

        // --- On-time return ---
        "7", "1",

        // --- Late return (Epson, due was 13 days ago) ---
        "7", "1",

        // --- Report ---
        "11",

        // --- Exit ---
        "12"
    ])
    : new SystemConsole();

var display = new Display(console);
var input = new Input(console, display);

var commands = new List<ICommand>
{
    new AddDeviceCommand(deviceService, input, display),
    new ListDevicesCommand(deviceService, display),
    new ListAvailableDevicesCommand(deviceService, display),
    new UpdateDeviceStatusCommand(deviceService, input, display),
    new RegisterUserCommand(userService, input, display),
    new RentDeviceCommand(rentalService, userService, deviceService, input, display),
    new ReturnDeviceCommand(rentalService, input, display),
    new ListRentalsCommand(rentalService, display),
    new ListUserRentalsCommand(rentalService, userService, input, display),
    new ListOverdueRentalsCommand(rentalService, display),
    new ReportCommand(deviceService, userService, rentalService, display),
    new ExitCommand()
};

var app = new App(commands, input, display);
app.Run();
