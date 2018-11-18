using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    internal class TemplateRenderer : ITemplateRenderer
    {
        public void Render(ITemplate template)
        {
            var doc = template.Create();
            ConsoleRenderer.RenderDocument(doc);
        }
    }
}