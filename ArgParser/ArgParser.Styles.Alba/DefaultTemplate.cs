using System.Collections.Generic;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplate : Template
    {
        public DefaultTemplate(IContext context, string parserId) : base(context)
        {
            ParserId = parserId.ThrowIfArgumentNull(nameof(parserId));
        }

        public override Document Create()
        {
            var parser = Context.ParserRepository.Get(ParserId);
            var chain = Context.PathToRoot(ParserId);
            var examples = chain.SelectMany(x => x.Help?.Examples ?? new List<Example>()).ToList();
            var vm = new DefaultTemplateVm
            {
                Context = Context,
                Parser = parser,
                Examples = examples,
                CurrentTheme = Theme.Warm
            };
            return ElementFactory.InflateTempalte<Document>("Views/DefaultTemplate.xaml", vm,
                typeof(DefaultTemplate).Assembly);
        }

        public IElementFactory ElementFactory { get; set; } = new ElementFactory();
        public string ParserId { get; set; }
    }
}