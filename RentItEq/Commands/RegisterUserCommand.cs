using RentItEq.Components;
using RentItEq.Services;
using RentItEq.Types;

namespace RentItEq.Commands;

public class RegisterUserCommand : ICommand
{
    private readonly UserService _userService;
    private readonly Input _input;
    private readonly Display _display;

    public string Name => "RegisterUser";

    public RegisterUserCommand(UserService userService, Input input, Display display)
    {
        _userService = userService;
        _input = input;
        _display = display;
    }

    public void Execute()
    {
        _display.Header("Register User");

        var name = _input.ReadString("First name");
        var surname = _input.ReadString("Surname");
        var type = _input.ReadEnum<UserType>("User type");

        var user = _userService.AddUser(name, surname, type);
        _display.Success($"{user.Name} {user.Surname} ({user.Type}) [{user.Uuid}]");
    }
}
