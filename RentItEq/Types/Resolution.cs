namespace RentItEq.Types;

public readonly record struct Resolution(int Width, int Height)
{
    public override string ToString() => $"{Width}x{Height}";
}