namespace Toy.Robot.Simulator.Tests
{
    using Toy.Robot.Simulator.RobotSystem.Implementation;
    using Xunit;
    using System;
    using FluentAssertions;
    using Toy.Robot.Simulator.Commands.Interface;
    using Toy.Robot.Simulator.Commands.Implementation;
    using Toy.Robot.Simulator.InputParamConvertor.Implementation;
    using Toy.Robot.Simulator.TableTop.Implementation;
    using System.Collections.Generic;
    using Toy.Robot.Simulator.Commands;
    using Toy.Robot.Simulator.RobotSystem.Interface;
    using Moq;

    public class RobotControlProcesserTests
    {
        private readonly IRobotControlProcesser sut;
        public RobotControlProcesserTests()
        {
            var commandExecutor = new List<ICommandExecutor>
            {
                new LeftCommandExecutor(),
                new MoveCommandExecutor(new ToyTable(5, 5))
            };
            var parameterConvertor = new ParameterConvertor();
            commandExecutor.Add(new PlaceCommandExecutor(parameterConvertor, new ToyTable(5, 5)));
            commandExecutor.Add(new RightCommandExecutor());

            var CommandStrategy = new CommandStrategy(commandExecutor);
            sut = new RobotControlProcesser(parameterConvertor, CommandStrategy);
        }

        [Fact]
        public void EmptyCommand_Returns_Exception_Test()
        {
            // Assign
            var parameter = "  3,3,NORTH";

            // Act & Assert
            sut.Invoking(x => x.ExecuteCommand(parameter))
                  .Should().Throw<ArgumentException>()
                  .WithMessage("Input command can not be null or empty");
        }

        [Fact]
        public void ExecuteCommand_Returns_Exception_Test()
        {
            // Assign
            var parameter = "INVALIDPLACE 3,3,NORTH";

            // Act & Assert
            sut.Invoking(x => x.ExecuteCommand(parameter))
                  .Should().Throw<ArgumentException>()
                  .WithMessage("Invalid command. Pass the valid input command, Example: PLACE or MOVE or LEFT or RIGHT or REPORT");
        }

        [Fact]
        public void ExecuteCommand_Invlid_Input_Parameter_Test()
        {
            // Assign
            var parameter = "PLACE NORTH";

            // Act & Assert
            sut.Invoking(x => x.ExecuteCommand(parameter))
                  .Should().Throw<ArgumentException>()
                  .WithMessage("Invalid input format, pass the valid input format, Example: PLACE X,Y,F");
        }

        [Fact]
        public void ExecuteCommand_Invlid_X_Position_Parameter_Test()
        {
            // Assign
            var parameter = "PLACE x,3,NORTH";

            // Act & Assert
            sut.Invoking(x => x.ExecuteCommand(parameter))
                  .Should().Throw<ArgumentException>()
                  .WithMessage("Invalid X parameter x, please pass the valid integer value between [0-5].");
        }

        [Fact]
        public void ExecuteCommand_Invlid_Y_Position_Parameter_Test()
        {
            // Assign
            var parameter = "PLACE 3,y,NORTH";

            // Act & Assert
            sut.Invoking(x => x.ExecuteCommand(parameter))
                  .Should().Throw<ArgumentException>()
                  .WithMessage("Invalid Y parameter y, please pass the valid integer value between [0-5].");
        }

        [Fact]
        public void ExecuteCommand_Invlid_Direction_Parameter_Test()
        {
            // Assign
            var parameter = "PLACE 3,1,NORTHEX";

            // Act & Assert
            sut.Invoking(x => x.ExecuteCommand(parameter))
                  .Should().Throw<ArgumentException>()
                  .WithMessage("Invalid direction NORTHEX. Pass the valid directions: NORTH or SOUTH or EAST or WEST");
        }

        [Fact]
        public void ExecuteCommand_OutOfTable_Test()
        {
            // Assign
            var parameter = "PLACE 7,3,NORTH";

            // Act 
            var expected = sut.ExecuteCommand(parameter);
            expected = sut.ExecuteCommand("REPORT");

            // Assert
            expected.Should().BeEquivalentTo("");
        }

        [Fact]
        public void ExecuteCommand_Move_Returns_Output_Test()
        {
            // Assign
            var parameter = "PLACE 0,0,NORTH";

            // Act 
            var expected = sut.ExecuteCommand(parameter);
            sut.ExecuteCommand("MOVE");
            expected = sut.ExecuteCommand("REPORT");

            // Assert
            expected.Should().BeEquivalentTo("Output: 0,1,NORTH");
        }

        [Fact]
        public void ExecuteCommand_Left_Returns_Output_Test()
        {
            // Assign
            var parameter = "PLACE 1,2,EAST";

            // Act 
            var expected = sut.ExecuteCommand(parameter);
            sut.ExecuteCommand("MOVE");
            sut.ExecuteCommand("MOVE");
            sut.ExecuteCommand("LEFT");
            sut.ExecuteCommand("MOVE");
            expected = sut.ExecuteCommand("REPORT");

            // Assert
            expected.Should().BeEquivalentTo("Output: 3,3,NORTH");
        }

        [Fact]
        public void ExecuteCommand_MoveAndLeft_Returns_Output_Test()
        {
            // Assign
            var parameter = "PLACE 0,0,NORTH";

            // Act 
            var expected = sut.ExecuteCommand(parameter);
            sut.ExecuteCommand("LEFT");
            expected = sut.ExecuteCommand("REPORT");

            // Assert
            expected.Should().BeEquivalentTo("Output: 0,0,WEST");
        }

        [Fact]
        public void ExecuteCommand_Returns_Valid_Output_Test()
        {
            // Assign
            var parameter = "PLACE 3,3,NORTH";

            // Act 
            sut.ExecuteCommand(parameter);
            var expected = sut.ExecuteCommand("REPORT");

            // Assert
            expected.Should().BeEquivalentTo("Output: 3,3,North");
        }
    }
}