using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParserHelpTemplate
    {
        public ParserHelpTemplate(IContext context, string parserId)
        {
            Context = context.ToAlbaContext();
            Parser = context.ParserRepository.Get(parserId);
            TemplateViewModel = CreateViewModel(context);
        }

        public Document Create()
        {
            using (var fs = File.OpenRead("ParserHelp.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, TemplateViewModel, new XamlElementReaderSettings
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

        private ParserHelpTemplateViewModel CreateViewModel(IContext context)
        {
            var vm = new ParserHelpTemplateViewModel();
            vm.Chain = context.PathToRoot(Parser.Id).Reverse().Select(x => new ParserViewModel
            {
                Parser = x,
                Theme = Context.Themes.TryGetValue(x, out var theme) ? theme : Theme.Default
            }).ToList();
            vm.ParameterVms = vm.Chain.SelectMany(x => x.Parser.Parameters.Select(y => new ParameterViewModel
            {
                Parameter = y,
                Theme = x.Theme
            })).ToList();
            return vm;
        }

        protected internal AlbaContext Context { get; set; }
        protected internal Parser Parser { get; set; }
        protected internal ParserHelpTemplateViewModel TemplateViewModel { get; set; }
    }
}