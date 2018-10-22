namespace ArgParser.Core.Help
{
    public interface IExample
    {
        string Name { get; }
        string ShortDescription { get; }
        string[] Usage { get; }
    }
}