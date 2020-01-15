namespace Toy.Robot.Simulator.TableTop.Interface
{
    using Toy.Robot.Simulator.Models;

    public interface IToyTable
    {
        bool CanMoveToNewPosition(Position newPosition);
    }
}
