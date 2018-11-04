namespace ArgParser.Core.Help.Dom
{
    public class HeadingNode : TextNode
    {
            
        public HeadingNode(string text) : base(text)
        {
        }

     
        public int Size { get; set; }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}