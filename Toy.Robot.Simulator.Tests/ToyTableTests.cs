namespace Toy.Robot.Simulator.Tests
{
    using Toy.Robot.Simulator.TableTop.Interface;
    using Xunit;
    using FluentAssertions;
    using Toy.Robot.Simulator.Models;
    using Toy.Robot.Simulator.TableTop.Implementation;
    using Moq;

    public class ToyTableTests 
    {
        private readonly IToyTable sut;

        public ToyTableTests() 
        {
            sut = new Mock<ToyTable>(5, 5).Object; 
        }

        [Fact]
        public void Check_Table_Position_Invalid()
        {
            // Assign
            var position = new Position(9, 7);

            // Act 
            var expected = sut.CanMoveToNewPosition(position);

            // Assert
            expected.Should().BeFalse();
        }

        [Fact]
        public void Check_Table_Position_valid()
        {
            // Assign
            var position = new Position(2, 3);
           
            // Act 
            var expected = sut.CanMoveToNewPosition(position);

            // Assert
            expected.Should().BeTrue();
        }
    }
}
