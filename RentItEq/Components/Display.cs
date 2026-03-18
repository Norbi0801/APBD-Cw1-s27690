namespace RentItEq.Components;

public static class Display
{
    public static void Header(string title)
    {
        Console.WriteLine($"\n--- {title} ---");
    }

    public static void Success(string message)
    {
        Console.WriteLine($"[OK] {message}");
    }

    public static void Error(string message)
    {
        Console.WriteLine($"[ERROR] {message}");
    }

    public static void Info(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }

    public static void ListItems<T>(IEnumerable<T> items, Func<T, string> formatter, string emptyMessage = "No items.")
    {
        var list = items.ToList();
        if (list.Count == 0)
        {
            Info(emptyMessage);
            return;
        }

        foreach (var item in list)
            Console.WriteLine($"  {formatter(item)}");
    }
}
