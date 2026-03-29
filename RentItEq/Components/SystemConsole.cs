namespace RentItEq.Components;

public class SystemConsole : IConsole
{
    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);
    public string? ReadLine() => Console.ReadLine();
}
