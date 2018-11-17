using System;
using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParserHelpTemplate
    {
        protected internal IContext Context { get; set; }
        protected internal Parser Parser { get; set; }
        protected internal ParserHelpTemplateViewModel TemplateViewModel { get; set; }

        public ParserHelpTemplate(IContext context, string parserId)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Parser = context.ParserRepository.Get(parserId);
            TemplateViewModel = CreateViewModel(context);
        }

        private ParserHelpTemplateViewModel CreateViewModel(IContext context)
        {
            var vm = new ParserHelpTemplateViewModel();
            vm.Chain = context.PathToRoot(Parser.Id).Reverse().Select(x => new ParserViewModel()
            {
                Parser = x,
                Theme = Theme.Warm
            }).ToList();
            vm.ParameterVms = vm.Chain.SelectMany(x => x.Parser.Parameters.Select(y => new ParameterViewModel()
            {
                Parameter = y,
                Theme = x.Theme
            })).ToList();
            return vm;
        }

        public Document Create()
        {
            using (var fs = File.OpenRead("ParserHelp.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, TemplateViewModel, new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(FigletDiv).Assembly,
                        typeof(ParserHelpTemplate).Assembly,
                        typeof(Parser).Assembly
                    }
                });
            }
        }
    }
}
