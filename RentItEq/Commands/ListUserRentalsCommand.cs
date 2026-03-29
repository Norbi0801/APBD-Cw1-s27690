using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListUserRentalsCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly UserService _userService;
    private readonly Input _input;
    private readonly Display _display;

    public string Name => "ListUserRentals";

    public ListUserRentalsCommand(RentalService rentalService, UserService userService, Input input, Display display)
    {
        _rentalService = rentalService;
        _userService = userService;
        _input = input;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Active Rentals by User");

        var user = _input.Select("Select user", _userService.GetUserList(),
            u => $"{u.Name} {u.Surname} ({u.Type})");

        _display.ListItems(
            _rentalService.GetActiveUserRentals(user.Uuid),
            r => $"[{r.Uuid}] {r.Device.Name} | rented {r.StartRentalDateTime:d}, due {r.DueDateTime:d}",
            "No active rentals for this user."
        );
    }
}
