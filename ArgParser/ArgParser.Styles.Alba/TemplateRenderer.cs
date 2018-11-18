using System.Diagnostics.CodeAnalysis;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    internal class TemplateRenderer : ITemplateRenderer
    {
        [ExcludeFromCodeCoverage]
        public void Render(ITemplate template)
        {
            var doc = template.Create();
            ConsoleRenderer.RenderDocument(doc);
        }
    }
}