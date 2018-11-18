using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IParseStrategyFactory
    {
        IParseStrategy Create();
    }
}