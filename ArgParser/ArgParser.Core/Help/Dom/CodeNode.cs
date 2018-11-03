using ArgParser.Core.Help.Dom;

namespace ArgParser.Core.Help
{
    public class CodeNode : TextNode
    {
            
        public CodeNode(string text) : base(text)
        {
        }

            
        public override void Accept(IHelpNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}