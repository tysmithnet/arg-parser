using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParameterGrid : BlockElement
    {
        public IContext Context { get; set; }
        public Parser Parser { get; set; }

        public override IEnumerable<Element> GenerateVisualElements()
        {
            var chain = Context.PathToRoot(Parser.Id);
            var parameters = chain.SelectMany(p => p.Parameters).Select(p => new ParameterVm()
            {
                Base = p,
                RequiredText = CreateRequiredText(p)
            });
            var vm = new ParameterGridVm()
            {
                Parameters = parameters.ToList()
            };
            using (var fs = File.OpenRead("Views/ParameterGrid.xaml"))
            {
                var element = ConsoleRenderer.ReadElementFromStream<Div>(fs, vm, new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(ParameterUsage).Assembly,
                    }
                });
                return element.ToEnumerableOfOne();
            }
        }

        public string CreateRequiredText(Parameter p)
        {
            if (p is IRequirable casted && casted.IsRequired)
                return "✓";
            return "";
        }
    }

    public class ParameterGridVm
    {
        public IList<ParameterVm> Parameters { get; set; }
    }

    public class ParameterVm
    {
        public Parameter Base { get; set; }
        public string RequiredText { get; set; }
    }
}
