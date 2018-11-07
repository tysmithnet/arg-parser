namespace ArgParser.Core.Help
{
    public interface IUsageFactory
    {
        TextNode Create(string parserId, IContext context);
    }
}