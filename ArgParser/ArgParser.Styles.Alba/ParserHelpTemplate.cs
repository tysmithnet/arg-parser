using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AlbaContext : IContext
    {
        protected internal IContext Context { get; set; }
        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;
        public IParserRepository ParserRepository => Context.ParserRepository;
        protected internal Dictionary<Parser, Theme> Theme { get; set; }
        
        public AlbaContext(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }

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

    public class ParserHelpTemplateViewModel
    {
        public ParserViewModel ParserVm => Chain.Last();
        public IList<ParserViewModel> Chain { get; set; }
        public IList<ParameterViewModel> ParameterVms { get; set; }

        public string BannerColor =>
            $"{ParserVm.Theme.DefaultTextColor} 0; {ParserVm.Theme.SecondaryTextColor}; {ParserVm.Theme.CodeColor} 3";

        public string SubTitle
        {
            get
            {
                var sb = new StringBuilder(ParserVm.Parser.Id);
                if (ParserVm.Parser.Help.Version.IsNotNullOrWhiteSpace())
                    sb.Append($" - {ParserVm.Parser.Help.Version}");
                if (ParserVm.Parser.Help.ShortDescription.IsNotNullOrWhiteSpace())
                    sb.Append($" - {ParserVm.Parser.Help.ShortDescription}");
                return sb.ToString();
            }
        }
        
    }

    public class ParserViewModel
    {
        public Parser Parser { get; set; }
        public Theme Theme { get; set; }
    }

    public class ParameterViewModel
    {
        public Parameter Parameter { get; set; }
        public Theme Theme { get; set; }

        public string RequiredText
        {
            get
            {
                if (Parameter is IRequirable casted && casted.IsRequired)
                    return "✓";
                return "";
            }
        }
    }
}
