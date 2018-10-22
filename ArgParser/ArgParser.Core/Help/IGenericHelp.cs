namespace ArgParser.Core.Help
{
    public interface IGenericHelp
    {
        string Version { get; }
        string Name { get; }
        string ShortDescription { get; }
        string Description { get; }
        string Author { get; }
    }
}