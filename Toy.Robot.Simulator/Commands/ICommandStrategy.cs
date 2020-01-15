namespace Toy.Robot.Simulator.Commands
{
    using Toy.Robot.Simulator.Enum;
    using Toy.Robot.Simulator.Models;

    public interface ICommandStrategy
    {
        string ExecuteCommand(Command commands, string locationParameter);
    }
}
