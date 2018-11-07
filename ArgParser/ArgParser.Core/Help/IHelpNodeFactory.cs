namespace ArgParser.Core.Help
{
    public interface IHelpNodeFactory
    {
        RootNode Create(string parserId, IContext context);
    }
}