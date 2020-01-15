namespace Toy.Robot.Simulator.Commands.Implementation
{
    using System.Runtime.InteropServices;
    using Toy.Robot.Simulator.Commands.Interface;
    using Toy.Robot.Simulator.Enum;
    using Toy.Robot.Simulator.Models;
    using Toy.Robot.Simulator.TableTop.Interface;

    public class MoveCommandExecutor : ICommandExecutor
    {
        private readonly IToyTable toyTable;
        public Command Command => Command.Move;

        public MoveCommandExecutor(IToyTable toyTable)
        {
            this.toyTable = toyTable;
        }

        public ToyLocation Operator(ToyLocation currentToyLocation, [Optional] string parameter)
        {
            var nextPosition = new Position(currentToyLocation.Position.X, currentToyLocation.Position.Y);
            switch (currentToyLocation.Direction)
            {
                case Direction.East:
                    nextPosition.X += 1;
                    break;
                case Direction.West:
                    nextPosition.X -= 1;
                    break;
                case Direction.North:
                    nextPosition.Y += 1;
                    break;
                case Direction.South:
                    nextPosition.Y -= 1;
                    break;
            }

            if (this.toyTable.CanMoveToNewPosition(nextPosition))
            {
                currentToyLocation.Position = nextPosition;
            }

            return currentToyLocation;
        }
    }
}
