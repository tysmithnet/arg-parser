using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Alba
{
    public class ExampleList : BlockElement
    {
        public IList<Example> Examples { get; set; }
        public IElementFactory ElementFactory { get; set; } = new ElementFactory();
        public override IEnumerable<Element> GenerateVisualElements()
        {
            if(!Examples.Any())
                return new Span().ToEnumerableOfOne();
            return ElementFactory.InflateTempalte<Div>("Views/ExampleList.xaml", this, typeof(Example).Assembly)
                .ToEnumerableOfOne();
        }
    }
}
