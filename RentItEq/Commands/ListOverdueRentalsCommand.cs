using RentItEq.Components;
using RentItEq.Services;

namespace RentItEq.Commands;

public class ListOverdueRentalsCommand : ICommand
{
    private readonly RentalService _rentalService;

    public string Name => "ListOverdueRentals";

    public ListOverdueRentalsCommand(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public void Execute()
    {
        Display.Header("Overdue Rentals");
        Display.ListItems(
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
