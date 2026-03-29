namespace RentItEq.Components;

public class Input
{
    private readonly IConsole _console;
    private readonly Display _display;

    public Input(IConsole console, Display display)
    {
        _console = console;
        _display = display;
    }

    public string ReadString(string label)
    {
        _console.Write($"{label}: ");
        return _console.ReadLine()?.Trim() ?? "";
    }

    public string? ReadOptional(string label)
    {
        _console.Write($"{label} (optional): ");
        var value = _console.ReadLine()?.Trim();
        return string.IsNullOrEmpty(value) ? null : value;
    }

    public int ReadInt(string label)
    {
        while (true)
        {
            _console.Write($"{label}: ");
            if (int.TryParse(_console.ReadLine()?.Trim(), out var result))
                return result;
            _display.Error("Invalid number. Try again.");
        }
    }

    public int ReadInt(string label, int defaultValue)
    {
        _console.Write($"{label} (default {defaultValue}): ");
        var input = _console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input)) return defaultValue;
        if (int.TryParse(input, out var result)) return result;
        _display.Error($"Invalid number, using default ({defaultValue}).");
        return defaultValue;
    }

    public double ReadDouble(string label)
    {
        while (true)
        {
            _console.Write($"{label}: ");
            if (double.TryParse(_console.ReadLine()?.Trim(), out var result))
                return result;
            _display.Error("Invalid number. Try again.");
        }
    }

    public Guid ReadGuid(string label)
    {
        while (true)
        {
            _console.Write($"{label}: ");
            if (Guid.TryParse(_console.ReadLine()?.Trim(), out var result))
                return result;
            _display.Error("Invalid UUID. Try again.");
        }
    }

    public T ReadEnum<T>(string label) where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        _console.WriteLine(label);
        for (var i = 0; i < values.Length; i++)
            _console.WriteLine($"  {i + 1}. {values[i]}");

        while (true)
        {
            _console.Write("> ");
            if (int.TryParse(_console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= values.Length)
                return values[choice - 1];
            _display.Error("Invalid choice. Try again.");
        }
    }

    public string ReadChoice(string label, params string[] options)
    {
        _console.WriteLine(label);
        for (var i = 0; i < options.Length; i++)
            _console.WriteLine($"  {i + 1}. {options[i]}");

        while (true)
        {
            _console.Write("> ");
            if (int.TryParse(_console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= options.Length)
                return options[choice - 1].ToLower();
            _display.Error("Invalid choice. Try again.");
        }
    }

    public T Select<T>(string label, List<T> items, Func<T, string> formatter)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("No items to select from.");

        _console.WriteLine(label);
        for (var i = 0; i < items.Count; i++)
            _console.WriteLine($"  {i + 1}. {formatter(items[i])}");

        while (true)
        {
            _console.Write("> ");
            if (int.TryParse(_console.ReadLine()?.Trim(), out var choice) && choice >= 1 && choice <= items.Count)
                return items[choice - 1];
            _display.Error("Invalid choice. Try again.");
        }
    }
}
