namespace ArgParser.HelpWriter
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