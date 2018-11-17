using System;
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
        public ConsoleColor GradientStart { get; set; }
        public ConsoleColor GradientMid { get; set; }
        public ConsoleColor GradientEnd { get; set; }
        
        public override IEnumerable<Element> GenerateVisualElements()
        {
            var div = new Div(new FigletDiv()
            {
                Text = Title,
                ColorGradient = new FigletGradient()
                {
                    GradientStops =
                    {
                        new FigletGradientStop(GradientStart, 0),
                        new FigletGradientStop(GradientMid),
                        new FigletGradientStop(GradientMid, 3)
                    }
                }
            });
            return div.ToEnumerableOfOne();
        }
    }
}