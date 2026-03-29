namespace RentItEq.Components;

public class ScriptedConsole : IConsole
{
    private readonly Queue<string> _inputs;

    public ScriptedConsole(IEnumerable<string> inputs)
    {
        _inputs = new Queue<string>(inputs);
    }

    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);

    public string? ReadLine()
    {
        if (_inputs.Count == 0)
            throw new InvalidOperationException("No more scripted inputs available");

        var input = _inputs.Dequeue();
        Console.WriteLine(input);
        return input;
    }
}
