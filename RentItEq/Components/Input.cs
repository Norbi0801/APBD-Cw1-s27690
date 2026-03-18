namespace RentItEq.Components;

public static class Input
{
    public static string ReadString(string label)
    {
        Console.Write($"{label}: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static string? ReadOptional(string label)
    {
        Console.Write($"{label} (optional): ");
        var value = Console.ReadLine()?.Trim();
        return string.IsNullOrEmpty(value) ? null : value;
    }

    public static int ReadInt(string label)
    {
        while (true)
        {
            Console.Write($"{label}: ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out var result))
                return result;
            Display.Error("Invalid number. Try again.");
        }
    }

    public static int ReadInt(string label, int defaultValue)
    {
        Console.Write($"{label} (default {defaultValue}): ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input)) return defaultValue;
        if (int.TryParse(input, out var result)) return result;
        Display.Error($"Invalid number, using default ({defaultValue}).");
        return defaultValue;
    }

    public static double ReadDouble(string label)
    {
        while (true)
        {
            Console.Write($"{label}: ");
            if (double.TryParse(Console.ReadLine()?.Trim(), out var result))
                return result;
            Display.Error("Invalid number. Try again.");
        }
    }

    public static Guid ReadGuid(string label)
    {
        while (true)
        {
            Console.Write($"{label}: ");
            if (Guid.TryParse(Console.ReadLine()?.Trim(), out var result))
                return result;
            Display.Error("Invalid UUID. Try again.");
        }
    }

    public static T ReadEnum<T>(string label) where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        Console.WriteLine(label);
        for (var i = 0; i < values.Length; i++)
            Console.WriteLine($"  {i + 1}. {values[i]}");

        while (true)
        {
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= values.Length)
                return values[choice - 1];
            Display.Error("Invalid choice. Try again.");
        }
    }

    public static string ReadChoice(string label, params string[] options)
    {
        Console.WriteLine(label);
        for (var i = 0; i < options.Length; i++)
            Console.WriteLine($"  {i + 1}. {options[i]}");

        while (true)
        {
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= options.Length)
                return options[choice - 1].ToLower();
            Display.Error("Invalid choice. Try again.");
        }
    }

    public static T Select<T>(string label, List<T> items, Func<T, string> formatter)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("No items to select from.");

        Console.WriteLine(label);
        for (var i = 0; i < items.Count; i++)
            Console.WriteLine($"  {i + 1}. {formatter(items[i])}");

        while (true)
        {
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= items.Count)
                return items[choice - 1];
            Display.Error("Invalid choice. Try again.");
        }
    }
}
