using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplate : Template<DefaultTemplateVm>
    {
        public DefaultTemplate(IContext context, DefaultTemplateVm viewModel) : base(context, viewModel)
        {
        }

        public override Document Create()
        {
            var parser = Context.ParserRepository.Get(ViewModel.ParserId);
            var chain = Context.PathToRoot(ViewModel.ParserId);
            var examples = chain.SelectMany(x => x.Help?.Examples ?? new List<Example>()).ToList();
            var theme = Theme.Default;
            if (Context.ParserThemes.ContainsKey(parser.Id))
                theme = Context.ParserThemes[parser.Id];
            var vm = new DefaultTemplateVm(parser, Context, theme)
            {
                Examples = examples,
            };
            return ElementFactory.InflateTempalte<Document>("Views/DefaultTemplate.xaml", vm,
                typeof(DefaultTemplate).Assembly);
        }

        public IElementFactory ElementFactory { get; set; } = new ElementFactory();
    }
}