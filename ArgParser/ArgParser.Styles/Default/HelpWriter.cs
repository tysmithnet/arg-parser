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
}