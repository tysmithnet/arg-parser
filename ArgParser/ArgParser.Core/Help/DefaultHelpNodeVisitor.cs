using System;
using ArgParser.Core.Help.Dom;

namespace ArgParser.Core.Help
{
    public class DefaultHelpNodeVisitor : IHelpNodeVisitor
    {
            
        public void Visit(TextNode node)
        {
            TextNode?.Invoke(node);
        }

            
        public void Visit(CodeNode node)
        {
            CodeNode?.Invoke(node);
        }

            
        public void Visit(HeadingNode node)
        {
            HeadingNode?.Invoke(node);
        }

            
        public void Visit(TableNode node)
        {
            TableNode?.Invoke(node);
            foreach (var tableRowNode in node.Rows) tableRowNode.Accept(this);
        }

            
        public void Visit(TableRowNode node)
        {
            TableRowNode?.Invoke(node);
            foreach (var dataNode in node.TableDataNodes) dataNode.Accept(this);
        }

            
        public void Visit(TableDataNode node)
        {
            TableDataNode?.Invoke(node);
        }

            
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