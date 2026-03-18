namespace RentItEq.Commands;

public class ExitCommand : ICommand
{
    public string Name => "Exit";

    public void Execute()
    {
        Environment.Exit(0);
    }
}
