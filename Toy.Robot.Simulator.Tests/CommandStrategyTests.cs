namespace Toy.Robot.Simulator.Tests
{
    using System.Collections.Generic;
    using Toy.Robot.Simulator.Commands.Implementation;
    using Toy.Robot.Simulator.Commands.Interface;
    using Toy.Robot.Simulator.TableTop.Implementation;
    using Xunit;
    using Toy.Robot.Simulator.Enum;
    using Toy.Robot.Simulator.Commands;
    using FluentAssertions;
    using Toy.Robot.Simulator.InputParamConvertor.Implementation;

    public class CommandStrategyTests 
    {
        private readonly List<ICommandExecutor> commandExecutor;
        private readonly CommandStrategy sut;

        public CommandStrategyTests() 
        {
            commandExecutor = new List<ICommandExecutor>();
            commandExecutor.Add(new LeftCommandExecutor());
            commandExecutor.Add(new MoveCommandExecutor(new ToyTable(5, 5)));
            var parameterConvertor = new ParameterConvertor();
            commandExecutor.Add(new PlaceCommandExecutor(parameterConvertor, new ToyTable(5, 5)));
            commandExecutor.Add(new RightCommandExecutor());

            sut = new CommandStrategy(commandExecutor);
        }

        [Fact]
        public void Place_Command_Invalid_Test()
        {
            // Assign
            var command = Command.Place;
            
            // Act 
            sut.ExecuteCommand(command, "PLACE 6,8,NORTH");

            // Assert
            sut.ToyLocation.Should().BeNull();
        }

        [Fact]
        public void Place_Command_valid_Test()
        {
            // Assign
            var command = Command.Place;

            // Act 
            sut.ExecuteCommand(command, "PLACE 0,0,NORTH");
            command = Command.Report;
            var result = sut.ExecuteCommand(command, "REPORT");

            // Assert
            result.Should().BeEquivalentTo("Output: 0,0,NORTH");
        }

        [Fact]
        public void Move_Command_Out_Table_Test()
        {
            // Assign
            var command = Command.Place;

            // Act 
            sut.ExecuteCommand(command, "Place 3,3,NORTH");
            command = Command.Move;
            sut.ExecuteCommand(command, "Move");
            sut.ExecuteCommand(command, "Move");
            sut.ExecuteCommand(command, "Move");
            sut.ExecuteCommand(command, "Move");
            sut.ExecuteCommand(command, "Move");
            command = Command.Report;
            var result = sut.ExecuteCommand(command, "REPORT");

            // Assert
            result.Should().BeEquivalentTo("Output: 3,4,NORTH");
        }

        [Fact]
        public void Right_Command_Valid_Test()
        {
            // Assign
            var command = Command.Place;

            // Act 
            sut.ExecuteCommand(command, "Place 0,0,NORTH");
            command = Command.Right;
            sut.ExecuteCommand(command, "Right");
            sut.ExecuteCommand(command, "Right");
            sut.ExecuteCommand(command, "Right");

            command = Command.Report;
            var result = sut.ExecuteCommand(command, "REPORT");

            // Assert
            result.Should().BeEquivalentTo("Output: 0,0,West");
        }

        [Fact]
        public void Left_Command_Valid_Test()
        {
            // Assign
            var command = Command.Place;

            // Act 
            sut.ExecuteCommand(command, "Place 0,0,NORTH");
            command = Command.Left;
            sut.ExecuteCommand(command, "Left");
            sut.ExecuteCommand(command, "Left");
            sut.ExecuteCommand(command, "Left");

            command = Command.Report;
            var result = sut.ExecuteCommand(command, "REPORT");

            // Assert
            result.Should().BeEquivalentTo("Output: 0,0,East");
        }
    }
}