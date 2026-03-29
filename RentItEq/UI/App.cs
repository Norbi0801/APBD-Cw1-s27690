using RentItEq.Commands;
using RentItEq.Components;

namespace RentItEq.UI;

public class App
{
    private readonly List<ICommand> _commands;
    private readonly Input _input;
    private readonly Display _display;

    public App(List<ICommand> commands, Input input, Display display)
    {
        
        
        _commands = commands;
        _input = input;
        _display = display;
    }

    public void Run()
    {
        while (true)
        {
            _display.Header("RentItEq - Equipment Rental System");

            var command = _input.Select("Select action", _commands, c => c.Name);

            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                _display.Error(ex.Message);
            }
        }
    }
}
