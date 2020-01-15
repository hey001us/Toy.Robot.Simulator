
namespace Toy.Robot.Simulator.InputParamConvertor.Implementation
{
    using Toy.Robot.Simulator.InputParamConvertor.Interface;
    using Toy.Robot.Simulator.Enum;
    using System;
    using Toy.Robot.Simulator.Models;
    using System.Linq;

    public class ParameterConvertor : IParameterConvertor
    {
        public string[] InputParameterConvertor(string inputParameter)
        {
            if (string.IsNullOrEmpty(inputParameter))
            {
                throw new ArgumentException($"Input parameter can not be null or empty");
            }

            var parameers = inputParameter.Split(' ');
            if (parameers.Length != 2)
            {
                throw new ArgumentException("Invalid input format, pass the valid input format, Example: PLACE X,Y,F");
            }
            return parameers;
        }

        public Command CommandConvertor(string inputCommand)
        {
            if (string.IsNullOrWhiteSpace(inputCommand))
            {
                throw new ArgumentException($"Input command can not be null or empty");
            }

            if (!Enum.TryParse(inputCommand, true, out Command command))
            {
                throw new ArgumentException("Invalid command. Pass the valid input command, Example: PLACE or MOVE or LEFT or RIGHT or REPORT");
            }

            return command;
        }

        public ToyLocation LocationConvertor(string inputLocation)
        {
            var locationParameers = inputLocation.Split(',');
            if (locationParameers.Length != 3)
            {
                throw new ArgumentException($"Invalid input format, pass the valid input format, Example: PLACE X,Y,F");
            }

            var location = inputLocation.Split(',');

            if(!int.TryParse(location[0], out int x))
            {
                throw new ArgumentException($"Invalid X parameter {location[0]}, please pass the valid integer value between [0-5].");
            }

            if (!int.TryParse(location[1], out int y))
            {
                throw new ArgumentException($"Invalid Y parameter {location[1]}, please pass the valid integer value between [0-5].");
            }
            var position = new Position(x, y);

            var directionString = location.Last();
            if (!Enum.TryParse(directionString, true, out Direction direction))
                throw new ArgumentException($"Invalid direction {directionString}. Pass the valid directions: NORTH or SOUTH or EAST or WEST");

            return new ToyLocation(direction, position);
        }
    }
}
