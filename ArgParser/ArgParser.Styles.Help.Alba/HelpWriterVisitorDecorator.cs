using System;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Help.Alba
{
    public class HelpWriterVisitorDecorator : IHelpNodeVisitor<Element>
    {
        public HelpWriterVisitor HelpWriterVisitor { get; protected internal set; }

        public HelpWriterVisitorDecorator(HelpWriterVisitor helpWriterVisitor)
        {
            HelpWriterVisitor = helpWriterVisitor ?? throw new ArgumentNullException(nameof(helpWriterVisitor));
        }

        public virtual Element Visit(HelpNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(RootNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(TextNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(HeadingNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(BlockNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(HorizontalLineNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(GridNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }

        public virtual Element Visit(CodeNode node)
        {
            return HelpWriterVisitor.Visit(node);
        }
    }
}