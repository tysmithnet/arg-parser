using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public interface IUsageFactory
    {
        HelpNode Create(string parserId, IContext context);
    }
}