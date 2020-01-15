namespace Toy.Robot.Simulator.Commands.Implementation
{
    using System.Runtime.InteropServices;
    using Toy.Robot.Simulator.Commands.Interface;
    using Toy.Robot.Simulator.Enum;
    using Toy.Robot.Simulator.Helper;
    using Toy.Robot.Simulator.Models;

    public class RightCommandExecutor : ICommandExecutor
    {
        public Command Command => Command.Right;

        public ToyLocation Operator(ToyLocation currentToyLocation, [Optional] string parameter)
        {
            currentToyLocation.Direction = currentToyLocation.Direction.Next();
            
            return currentToyLocation;
        }
    }
}
