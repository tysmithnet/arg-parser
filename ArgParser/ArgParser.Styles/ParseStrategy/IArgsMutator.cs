namespace ArgParser.Styles.ParseStrategy
{
    public interface IArgsMutator : IParseStrategyUnit
    {
        string[] Mutate(MutateArgsRequest request);
    }
}