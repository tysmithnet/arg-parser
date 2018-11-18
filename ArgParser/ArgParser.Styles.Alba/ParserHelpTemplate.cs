using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class ParserHelpTemplate : ITemplate
    {
        public ParserHelpTemplate(IContext context, string parserId)
        {
            Context = context.ToAlbaContext();
            Parser = context.ParserRepository.Get(parserId);
            ViewModel = CreateViewModel(context);
        }

        public Document Create()
        {
            using (var fs = File.OpenRead("ParserHelp.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, ViewModel, new XamlElementReaderSettings
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
            var albaContext = context.ToAlbaContext();
            var vm = new ParserHelpTemplateViewModel
            {
                Chain = albaContext.PathToRoot(Parser.Id).Reverse().Select(x => new ParserViewModel
                {
                    Parser = x,
                    Theme = albaContext.ThemeRepository.Get(x.Id)
                }).ToList()
            };
            vm.ParameterVms = vm.Chain.SelectMany(x => x.Parser.Parameters.Select(y => new ParameterViewModel
            {
                Parameter = y,
                Theme = x.Theme
            })).ToList();
            return vm;
        }

        protected internal AlbaContext Context { get; set; }
        protected internal Parser Parser { get; set; }
        protected internal ParserHelpTemplateViewModel ViewModel { get; set; }
    }
}