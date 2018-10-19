namespace ArgParser.Core.Help
{
    public abstract class Node
    {
        public abstract void Accept(INodeVisitor visitor);
    }
}