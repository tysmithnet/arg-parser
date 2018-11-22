using System;
using System.Collections.Generic;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using Figgle;

namespace ArgParser.Styles.Alba
{
    public class BannerArtDiv : BlockElement
    {
        public string Text { get; set; }
        public FiggleFont Font { get; set; }
        public override IEnumerable<Element> GenerateVisualElements()
        {
            string text = Font.Render(Text);
            return new Span(text)
            {
                Color = Color
            }.ToEnumerableOfOne();
        }
    }
}
