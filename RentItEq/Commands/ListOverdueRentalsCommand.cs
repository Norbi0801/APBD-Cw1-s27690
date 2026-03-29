using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListOverdueRentalsCommand : ICommand
{
    private readonly RentalService _rentalService;
    private readonly Display _display;

    public string Name => "ListOverdueRentals";

    public ListOverdueRentalsCommand(RentalService rentalService, Display display)
    {
        _rentalService = rentalService;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Overdue Rentals");
        _display.ListItems(
            _rentalService.GetOverdueRentals(),
            r =>
            {
                var overdueDays = (DateTime.Now - r.DueDateTime).Days;
                return $"[{r.Uuid}] {r.Renter.Name} {r.Renter.Surname} -> {r.Device.Name} | {overdueDays} days overdue";
            },
            "No overdue rentals."
        );
    }
}
