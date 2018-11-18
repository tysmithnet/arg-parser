using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public interface IParseStrategyFactory
    {
        IParseStrategy Create();
    }
}