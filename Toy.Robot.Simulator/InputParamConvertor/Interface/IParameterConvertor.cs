namespace Toy.Robot.Simulator.InputParamConvertor.Interface
{
    using Toy.Robot.Simulator.Enum;
    using Toy.Robot.Simulator.Models;

    public interface IParameterConvertor
    {
        string[] InputParameterConvertor(string inputParameter);

        Command CommandConvertor(string inputCommand);

        ToyLocation LocationConvertor(string inputLocation);
    }
}
