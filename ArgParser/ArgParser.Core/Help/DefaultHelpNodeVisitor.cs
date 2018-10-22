using System;

namespace ArgParser.Core.Help
{
    public class DefaultHelpNodeVisitor : IHelpNodeVisitor
    {
        /// <inheritdoc />
        public void Visit(TextNode node)
        {
            TextNode?.Invoke(node);
        }

        /// <inheritdoc />
        public void Visit(CodeNode node)
        {
            CodeNode?.Invoke(node);
        }

        /// <inheritdoc />
        public void Visit(HeadingNode node)
        {
            HeadingNode?.Invoke(node);
        }

        /// <inheritdoc />
        public void Visit(TableNode node)
        {
            TableNode?.Invoke(node);
            foreach (var tableRowNode in node.Rows) tableRowNode.Accept(this);
        }

        /// <inheritdoc />
        public void Visit(TableRowNode node)
        {
            TableRowNode?.Invoke(node);
            foreach (var dataNode in node.TableDataNodes) dataNode.Accept(this);
        }

        /// <inheritdoc />
        public void Visit(TableDataNode node)
        {
            TableDataNode?.Invoke(node);
        }

        /// <inheritdoc />
        public void Visit(RootNode node)
        {
            RootNode?.Invoke(node);
            foreach (var nodeChild in node.Children) nodeChild.Accept(this);
        }

        public Action<CodeNode> CodeNode { get; set; }
        public Action<HeadingNode> HeadingNode { get; set; }
        public Action<RootNode> RootNode { get; set; }
        public Action<TableDataNode> TableDataNode { get; set; }
        public Action<TableNode> TableNode { get; set; }
        public Action<TableRowNode> TableRowNode { get; set; }
        public Action<TextNode> TextNode { get; set; }
    }
}