using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplate : Template
    {
        public string ParserId { get; set; }
        
        public DefaultTemplate(IContext context, string parserId) : base(context)
        {
            ParserId = parserId.ThrowIfArgumentNull(nameof(parserId));
        }

        public override Document Create()
        {
            var parser = Context.ParserRepository.Get(ParserId);
            var chain = Context.PathToRoot(ParserId);
            var examples = chain.SelectMany(x => x.Help?.Examples ?? new List<Example>()).ToList();
            var vm = new DefaultTemplateVm()
            {
                Context = Context,
                Parser = parser,
                Examples = examples
            };

            using (var fs = File.OpenRead("Views/DefaultTemplate.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, vm, new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(DefaultTemplate).Assembly
                    }
                });
            }
        }
    }
}