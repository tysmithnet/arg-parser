namespace ArgParser.Core.Help
{
    public class TableRowNode : IHelpNode
    {
        /// <inheritdoc />
        public virtual void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}