namespace Toy.Robot.Simulator.RobotSystem.Implementation
{
    using System.Linq;
    using Toy.Robot.Simulator.Commands;
    using Toy.Robot.Simulator.InputParamConvertor.Interface;
    using Toy.Robot.Simulator.RobotSystem.Interface;

    public class RobotControlProcesser : IRobotControlProcesser
    {
        private readonly IParameterConvertor parameterConvertor;
        private readonly ICommandStrategy commandStrategy;


        public RobotControlProcesser(IParameterConvertor parameterConvertor, ICommandStrategy commandStrategy)
        {
            this.parameterConvertor = parameterConvertor;
            this.commandStrategy = commandStrategy;
        }

        public string ExecuteCommand(string inputParameter)
        {
            var input = inputParameter.Split(' ').FirstOrDefault();
            var command = this.parameterConvertor.CommandConvertor(input);

            var result = this.commandStrategy.ExecuteCommand(command, inputParameter);

            return result;
        }
    }
}
