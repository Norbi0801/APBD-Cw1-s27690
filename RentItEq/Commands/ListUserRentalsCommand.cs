using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListUserRentalsCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly UserService _userService;

    public string Name => "ListUserRentals";

    public ListUserRentalsCommand(RentalService rentalService, UserService userService)
    {
        _rentalService = rentalService;
        _userService = userService;
    }

    public void Execute()
    {
        Display.Header("Active Rentals by User");

        var user = Input.Select("Select user", _userService.GetUserList(),
            u => $"{u.Name} {u.Surname} ({u.Type})");

        Display.ListItems(
            _rentalService.GetActiveUserRentals(user.Uuid),
            r => $"[{r.Uuid}] {r.Device.Name} | rented {r.StartRentalDateTime:d}, due {r.DueDateTime:d}",
            "No active rentals for this user."
        );
    }
}
