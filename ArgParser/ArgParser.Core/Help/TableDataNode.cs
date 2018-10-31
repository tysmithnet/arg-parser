namespace ArgParser.Core.Help
{
    public class TableDataNode : TextNode
    {
            
        public TableDataNode(string text) : base(text)
        {
        }

            
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}