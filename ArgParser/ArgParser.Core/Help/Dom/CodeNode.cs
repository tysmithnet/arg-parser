using ArgParser.Core.Help.Dom;

namespace ArgParser.Core.Help
{
    public class CodeNode : TextNode
    {
            
        public CodeNode(string text) : base(text)
        {
        }

        public override T Accept<T>(IHelpNodeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}