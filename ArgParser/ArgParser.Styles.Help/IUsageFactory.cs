using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public interface IUsageFactory
    {
        TextNode Create(string parserId, IContext context);
    }
}