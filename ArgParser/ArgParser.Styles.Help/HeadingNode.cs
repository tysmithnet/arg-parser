namespace ArgParser.Styles.Help
{
    public class HeadingNode : TextNode
    {
        public HeadingNode(string text, int size = 1) : base(text)
        {
        }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor) => visitor.Visit(this);
    }
}