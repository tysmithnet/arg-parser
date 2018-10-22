namespace ArgParser.Core.Help
{
    public interface IHelpNode
    {
        void Accept(IHelpNodeVisitor visitor);
    }
}