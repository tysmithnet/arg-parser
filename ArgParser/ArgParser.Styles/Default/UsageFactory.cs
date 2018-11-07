using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class UsageFactory : IUsageFactory
    {
        protected internal string ParserId { get; set; }
        protected internal IContext Context { get; set; }

        public UsageFactory(string parserId, IContext context)
        {
            ParserId = parserId ?? throw new ArgumentNullException(nameof(parserId));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private CodeNode C(string code) => new CodeNode(code);
        private TextNode T(string text) => new TextNode(text);
        

        public TextNode Create()
        {
            // get all sub commands
            // get all parameters
            // [com1|com2|com3] [-abcde] [-fgh v1..v2] [-jk v1..v3] [p1] [p1..p2] [p1..pN]
            var subCommands = Context.HierarchyRepository.GetChildren(ParserId).ToList();
            var parser = Context.ParserRepository.Get(ParserId);

            var parameters = parser.Parameters.ToList();
            var booleans = parameters.OfType<BooleanSwitch>().ToList();

            var booleansWithLetter = booleans.Where(b => b.Letter.HasValue).GroupBy(b => b.MaxAllowed).ToList();
            var booleansWithWord = booleans.Where(b => !b.Letter.HasValue && b.Word != null).GroupBy(b => b.MaxAllowed).ToList();

            var singleSwitches = parameters.OfType<SingleValueSwitch>().ToList();
            var singlesWithLetters = singleSwitches.Where(b => b.Letter.HasValue).GroupBy(b => b.MaxAllowed).ToList();
            var singlesWithWords = singleSwitches.Where(b => !b.Letter.HasValue && b.Word != null).GroupBy(b => b.MaxAllowed).ToList();
            var valuesSwitches = parameters.OfType<ValuesSwitch>().ToList();
            var valuesWithLetters = valuesSwitches.Where(b => b.Letter.HasValue).GroupBy(b => b.MaxAllowed).ToList();
            var valuesWithWords = valuesSwitches.Where(b => !b.Letter.HasValue && b.Word != null).GroupBy(b => b.MaxAllowed).ToList();
            var positionals = parameters.OfType<Positional>().GroupBy(b => b.MaxAllowed).ToList();
        }

        protected internal virtual TextNode CreateSubCommandNode()
        {
            var subCommands = Context.HierarchyRepository.GetChildren(ParserId).ToList();
            var parser = Context.ParserRepository.Get(ParserId);


        }
    }
}
