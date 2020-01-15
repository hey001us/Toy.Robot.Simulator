using Toy.Robot.Simulator.Models;

namespace Toy.Robot.Simulator.Commands.Interface
{
    using System.Runtime.InteropServices;
    using Toy.Robot.Simulator.Enum;

    public interface ICommandExecutor
    {
        Command Command { get; }

        ToyLocation Operator(ToyLocation currentToyLocation, [Optional] string parameter);
    }
}
