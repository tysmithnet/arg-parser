using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IIterationInfoFactory : IParseStrategyUnit
    {
        IterationInfo Create(IterationInfoRequest request);
    }
}