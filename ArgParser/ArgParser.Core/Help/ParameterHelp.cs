namespace ArgParser.Core.Help
{
    public class ParameterHelp : SimpleHelp
    {
    }

    public interface IUsageFactory
    {
        TextNode Create(string parserId, IContext context);
    }
}
