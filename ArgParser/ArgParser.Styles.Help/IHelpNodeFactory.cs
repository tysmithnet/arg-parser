using ArgParser.Core;

namespace ArgParser.Styles.Help
{
    public interface IHelpNodeFactory
    {
        RootNode Create(string parserId, IContext context);
    }
}