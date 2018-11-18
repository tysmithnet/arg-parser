using System.Diagnostics.CodeAnalysis;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    [ExcludeFromCodeCoverage]
    internal class TemplateRenderer : ITemplateRenderer
    {
        public void Render(ITemplate template)
        {
            var doc = template.Create();
            ConsoleRenderer.RenderDocument(doc);
        }
    }
}