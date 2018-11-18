using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface IParseStrategyUnit
    {
        IContext Context { get; set; }
    }

    public interface IArgsMutator : IParseStrategyUnit
    {
        string[] Mutate(MutateArgsRequest request);
    }
}