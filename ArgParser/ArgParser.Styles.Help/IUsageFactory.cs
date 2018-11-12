using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public interface IUsageFactory
    {
        HelpNode CreateFullUsage(string parserId, IContext context);
        HelpNode CreatePositionalUsage(Positional positional, IContext context);
        HelpNode CreateSubCommandUsage(string parserId, IContext context);
        HelpNode CreateSwitchUsage(Switch @switch, IContext context);
    }
}