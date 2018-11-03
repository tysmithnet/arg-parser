namespace ArgParser.Core.Help.Dom
{
    public interface IHelpNode
    {
        void Accept(IHelpNodeVisitor visitor);
    }
}