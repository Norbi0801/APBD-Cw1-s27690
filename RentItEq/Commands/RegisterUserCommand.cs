using RentItEq.Components;
using RentItEq.Services;
using RentItEq.Types;

namespace RentItEq.Commands;

public class RegisterUserCommand : ICommand
{
    private readonly UserService _userService;

    public string Name => "RegisterUser";

    public RegisterUserCommand(UserService userService)
    {
        _userService = userService;
    }

    public void Execute()
    {
        Display.Header("Register User");

        var name = Input.ReadString("First name");
        var surname = Input.ReadString("Surname");
        var type = Input.ReadEnum<UserType>("User type");

        var user = _userService.AddUser(name, surname, type);
        Display.Success($"{user.Name} {user.Surname} ({user.Type}) [{user.Uuid}]");
    }
}
