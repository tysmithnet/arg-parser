namespace ArgParser.Styles
{
    public interface IArgsMutator
    {
        string[] Mutate(MutateArgsRequest request);
    }
}