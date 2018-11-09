using System.Text;

namespace ArgParser.Styles.Help
{
    public class HelpWriterVisitor : IHelpNodeVisitor<object>
    {
        protected internal StringBuilder Builder { get; set; } = new StringBuilder();

        public override string ToString()
        {
            return Builder.ToString();
        }

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
            if (Builder.Length > 0 && Builder[Builder.Length - 1] != '\n') 
                Builder.AppendLine();
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }
            Builder.AppendLine();
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
