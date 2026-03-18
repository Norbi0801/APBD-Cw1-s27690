using RentItEq.Commands;
using RentItEq.Components;

namespace RentItEq.UI;

public class App
{
    private readonly List<ICommand> _commands;

    public App(List<ICommand> commands)
    {
        _commands = commands;
    }

    public void Run()
    {
        while (true)
        {
            Display.Header("RentItEq - Equipment Rental System");

            var command = Input.Select("Select action", _commands, c => c.Name);

            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                Display.Error(ex.Message);
            }
        }
    }
}
