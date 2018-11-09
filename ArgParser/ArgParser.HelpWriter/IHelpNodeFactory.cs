using ArgParser.Core;

namespace ArgParser.HelpWriter
{
    public interface IHelpNodeFactory
    {
        RootNode Create(string parserId, IContext context);
    }
}