using System.IO;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    public class DefaultHelpTemplate : IHelpTemplate<DefaultViewModel>
    {
        public void Render(DefaultViewModel viewModel)
        {
            using (var fs = File.OpenRead("Views/Default.xaml"))
            {
                var document = ConsoleRenderer.ReadDocumentFromStream(fs, viewModel);
                ConsoleRenderer.RenderDocument(document);
            }
        }
    }
}