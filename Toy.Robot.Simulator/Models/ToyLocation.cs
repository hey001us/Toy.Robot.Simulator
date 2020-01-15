namespace Toy.Robot.Simulator.Models
{
    using Toy.Robot.Simulator.Enum;

    public class ToyLocation
    {
        public Direction Direction;

        public Position Position;

        public ToyLocation(Direction direction, Position position)
        {
            this.Direction = direction;
            this.Position = position;
        }
    }
}
