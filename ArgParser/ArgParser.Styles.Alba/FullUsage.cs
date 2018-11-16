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
        public override IEnumerable<Element> GenerateVisualElements()
        {
            var chain = Context.PathToRoot(Parser.Id).Reverse();
            
            var vm = new FullUsageVm()
            {
                ParserChain = chain.ToList(),
                Parameters = Parser.Parameters.ToList()
            };
            using (var fs = File.OpenRead("Views/FullUsage.xaml"))
            {
                var element = ConsoleRenderer.ReadElementFromStream<Div>(fs, vm, new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(Parameter).Assembly,
                        typeof(ParameterUsage).Assembly
                    }
                });
                return element.ToEnumerableOfOne();
            }
        }
    }

    public class FullUsageVm
    {
        public IList<Parser> ParserChain { get; set; }
        public IList<Parameter> Parameters { get; set; }
    }
}