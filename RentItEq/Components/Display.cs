namespace RentItEq.Components;

public class Display
{
    private readonly IConsole _console;

    public Display(IConsole console)
    {
        _console = console;
    }

    public void Header(string title)
    {
        _console.WriteLine($"\n--- {title} ---");
    }

    public void Success(string message)
    {
        _console.WriteLine($"[OK] {message}");
    }

    public void Error(string message)
    {
        _console.WriteLine($"[ERROR] {message}");
    }

    public void Info(string message)
    {
        _console.WriteLine($"[INFO] {message}");
    }

    public void ListItems<T>(IEnumerable<T> items, Func<T, string> formatter, string emptyMessage = "No items.")
    {
        var list = items.ToList();
        if (list.Count == 0)
        {
            Info(emptyMessage);
            return;
        }

        foreach (var item in list)
            _console.WriteLine($"  {formatter(item)}");
    }
}
