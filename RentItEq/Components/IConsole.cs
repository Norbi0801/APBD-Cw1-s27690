namespace RentItEq.Components;

public interface IConsole
{
    void Write(string text);
    void WriteLine(string text);
    string? ReadLine();
}
