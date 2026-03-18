namespace RentItEq.Commands;

public interface ICommand
{
    string Name { get; }
    void Execute();
}
