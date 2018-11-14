using System.Collections.Generic;
using System.IO;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;

namespace ArgParser.Styles.Alba
{
    public class AsciiArtBanner : BlockElement
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public override IEnumerable<Element> GenerateVisualElements()
        {
            var visualChildren = new List<Element>();
            using (var fs = File.OpenRead("Views/AsciiArtBanner.xaml"))
            {
                var element = ConsoleRenderer.ReadElementFromStream<Div>(fs, this, new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(FigletDiv).Assembly,
                        typeof(AsciiArtBanner).Assembly
                    }
                });
                visualChildren.Add(element);
            }

            return visualChildren;
        }
    }
}