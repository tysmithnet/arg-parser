using System.Collections.Generic;
using System.IO;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AsciiArtBanner : BlockElement
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IElementFactory ElementFactory { get; set; } = new ElementFactory();
        public override IEnumerable<Element> GenerateVisualElements()
        {
            var element = ElementFactory.InflateTempalte<Div>("Views/AsciiArtBanner.xaml", this,
                typeof(FigletDiv).Assembly,
                typeof(AsciiArtBanner).Assembly);
            return element.ToEnumerableOfOne();
        }
    }
}