using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class FullUsage : BlockElement
    {
        public IContext Context { get; set; }
        public Parser Parser { get; set; }
        public IElementFactory ElementFactory { get; set; } = new ElementFactory();
        public override IEnumerable<Element> GenerateVisualElements()
        {
            var chain = Context.PathToRoot(Parser.Id).Reverse();
            
            var vm = new FullUsageVm()
            {
                ParserChain = chain.ToList(),
                Parameters = Parser.Parameters.ToList()
            };
            return ElementFactory.InflateTempalte<Div>("Views/FullUsage.xaml", vm, typeof(Parameter).Assembly,
                typeof(ParameterUsage).Assembly).ToEnumerableOfOne();
        }
    }
}