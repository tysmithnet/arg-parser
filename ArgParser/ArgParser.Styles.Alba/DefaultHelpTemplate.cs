using System.IO;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;

namespace ArgParser.Styles.Alba
{
    public class DefaultHelpTemplate : IHelpTemplate<DefaultViewModel>
    {
        public void Render(DefaultViewModel viewModel)
        {
            using (var fs = File.OpenRead("Views/Default.xaml"))
            {
                var document = ConsoleRenderer.ReadDocumentFromStream(fs, viewModel, new XamlElementReaderSettings
                {
                    ReferenceAssemblies = {typeof(FigletDiv).Assembly}
                });
                ConsoleRenderer.RenderDocument(document);
            }
        }
    }
}