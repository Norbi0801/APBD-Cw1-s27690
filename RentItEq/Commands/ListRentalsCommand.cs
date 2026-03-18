using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListRentalsCommand : ICommand
{
    private readonly RentalService _rentalService;

    public string Name => "ListRentals";

    public ListRentalsCommand(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public void Execute()
    {
        Display.Header("All Rentals");
        Display.ListItems(
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
