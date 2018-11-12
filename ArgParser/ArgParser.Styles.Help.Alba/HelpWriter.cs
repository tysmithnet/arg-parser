using System.Diagnostics.CodeAnalysis;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Help.Alba
{
    public class HelpWriter : IHelpWriter
    {
        public string CreateHelpText(RootNode rootNode, int width = 80)
        {
            var visitor = new HelpWriterVisitor();
            var doc = (Document) visitor.Visit(rootNode);
            var text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget());
            return text.Length > 0 ? text.Remove(text.Length - 2) : text;
        }

        [ExcludeFromCodeCoverage]
        public void RenderHelp(RootNode rootNode, int width = 80)
        {
            var doc = CreateDocument(rootNode);
            ConsoleRenderer.RenderDocument(doc);
        }

        protected internal virtual Document CreateDocument(RootNode rootNode)
        {
            var visitor = new HelpWriterVisitor();
            var doc = (Document) visitor.Visit(rootNode);
            return doc;
        }
    }
}