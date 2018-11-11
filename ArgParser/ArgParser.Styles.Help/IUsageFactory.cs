using ArgParser.Core;
using ArgParser.Styles.Default;

namespace ArgParser.Styles.Help
{
    public interface IUsageFactory
    {
        HelpNode CreateFullUsage(string parserId, IContext context);
        HelpNode CreateSwitchUsage(Switch @switch, IContext context);
        HelpNode CreatePositionalUsage(Positional parameter, IContext context);
    }
}