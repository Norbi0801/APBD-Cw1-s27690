namespace RentItEq.Types;

public readonly record struct Money(decimal Amount, string Currency = "PLN")
{
    public override string ToString() => $"{Amount:N2} {Currency}";
}