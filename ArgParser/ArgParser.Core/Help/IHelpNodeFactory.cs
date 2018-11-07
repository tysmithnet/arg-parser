namespace ArgParser.Core.Help
{
    public interface IHelpNodeFactory
    {
        RootNode Create(IContext context, string parserId);
    }
}