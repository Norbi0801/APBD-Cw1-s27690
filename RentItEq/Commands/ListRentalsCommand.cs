using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListRentalsCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly Display _display;

    public string Name => "ListRentals";

    public ListRentalsCommand(RentalService rentalService, Display display)
    {
        _rentalService = rentalService;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("All Rentals");
        _display.ListItems(
            _rentalService.GetAllRentals(),
            r =>
            {
                var status = r.IsReturned() ? $"Returned {r.ReturnDateTime:g}" : $"Active, due {r.DueDateTime:d}";
                return $"[{r.Uuid}] {r.Renter.Name} {r.Renter.Surname} -> {r.Device.Name} | {status}";
            },
            "No rentals."
        );
    }
}
