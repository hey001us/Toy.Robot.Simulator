namespace Toy.Robot.Simulator.TableTop.Implementation
{
    using Toy.Robot.Simulator.Models;
    using Toy.Robot.Simulator.TableTop.Interface;

    public class ToyTable : IToyTable
    {
        public readonly int Width;
        public readonly int Depth;

        public ToyTable(int width, int depth)
        {
            this.Width = width;
            this.Depth = depth;
        }

        public bool CanMoveToNewPosition(Position newPosition) => newPosition.Y > -1
                    && newPosition.Y < Depth
                    && newPosition.X > -1
                    && newPosition.X < Width;
    }
}
