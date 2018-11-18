namespace ArgParser.Styles
{
    public interface IArgsMutator : IParseStrategyUnit
    {
        string[] Mutate(MutateArgsRequest request);
    }
}