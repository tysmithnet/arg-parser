using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public interface IIterationInfoFactory : IParseStrategyUnit
    {
        IterationInfo Create(IterationInfoRequest request);
    }
}