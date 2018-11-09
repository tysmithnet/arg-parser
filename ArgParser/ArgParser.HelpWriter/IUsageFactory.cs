namespace ArgParser.HelpWriter
{
    public interface IUsageFactory
    {
        TextNode Create(string parserId, IContext context);
    }
}