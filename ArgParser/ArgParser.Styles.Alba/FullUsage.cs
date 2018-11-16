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
        public string ParserId { get; set; }
        public override IEnumerable<Element> GenerateVisualElements()
        {
            var parser = Context.ParserRepository.Get(ParserId);
            var vm = new FullUsageVm()
            {
                Parameters = parser.Parameters.ToList()
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
        public IList<Parameter> Parameters { get; set; }
    }
}