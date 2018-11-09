using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class HelpWriter
    {
        public string CreateHelp(RootNode rootNode)
        {
            var visitor = new HelpWriterVisitor();
            rootNode.Accept(visitor);
            return visitor.Builder.ToString();
        }
    }

    public class HelpWriterVisitor : IHelpNodeVisitor<object>
    {
        protected internal StringBuilder Builder { get; set; } = new StringBuilder();

        public object Visit(HelpNode node)
        {
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }

            return null;
        }

        public object Visit(RootNode node)
        {
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }

            return null;
        }

        public object Visit(TextNode node)
        {
            Builder.Append(node.Text);
            return null;
        }

        public object Visit(HeadingNode node)
        {
            Builder.AppendLine(node.Text);
            return null;
        }

        public object Visit(BlockNode node)
        {
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }

            return null;
        }

        public object Visit(HorizontalLineNode node)
        {
            Builder.AppendLine("------------------------------");
            return null;
        }

        public object Visit(GridNode node)
        {
            int n = node.Columns;
            for (int i = 0; i < node.Children.Count; i++)
            {
                if (i % n == 0)
                    Builder.AppendLine();
                if (node.Children[i] is TextNode casted)
                    Builder.Append($"\t{casted.Text}");
            }
            return null;
        }

        public object Visit(CodeNode node)
        {
            Builder.Append(node.Text);
            return null;
        }
    }
}
