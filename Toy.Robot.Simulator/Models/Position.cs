using Toy.Robot.Simulator.TableTop.Implementation;

namespace Toy.Robot.Simulator.Models
{
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
