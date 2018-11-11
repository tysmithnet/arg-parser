﻿using System.Linq;
using System.Text;
using ArgParser.Core;
using ArgParser.Styles.Default;

namespace ArgParser.Styles.Help
{
    public class UsageFactory : IUsageFactory
    {
        public HelpNode Create(string parserId, IContext context)
        {
            parserId.ThrowIfArgumentNull(nameof(parserId));
            context.ThrowIfArgumentNull(nameof(context));
            var sb = new StringBuilder(parserId);
            var thisParserToRoot = context.PathToRoot(parserId);
            var parameters = thisParserToRoot.SelectMany(p => p.Parameters).ToList();

            var subCommands = context.HierarchyRepository.GetChildren(parserId).ToList();
            if (subCommands.Any())
                sb.Append($" [{subCommands.Join("|")}]");

            var booleans = parameters.OfType<BooleanSwitch>().ToList();
            var booleansWithLetter = booleans.Where(b => b.Letter.HasValue).ToList();
            if (booleansWithLetter.Any())
            {
                var inner = booleansWithLetter.Select(x => $"{x.Letter}").OrderBy(x => x);
                sb.Append($" [-{inner.Join("")}]");
            }

            var booleansWithWord = booleans.Where(b => !b.Letter.HasValue && b.Word != null).ToList();
            if (booleansWithWord.Any())
                sb.Append($" [--{booleansWithWord.Select(x => x.Word).OrderBy(x => x).Join("|")}]");

            var singleSwitches = parameters.OfType<SingleValueSwitch>().ToList();
            var singlesWithLetters = singleSwitches.Where(b => b.Letter.HasValue).ToList();
            if (singlesWithLetters.Any())
            {
                var inner = singlesWithLetters.Select(x => $"{x.Letter}").OrderBy(x => x);
                sb.Append($" [-{inner.Join("")} v1]");
            }

            var singlesWithWords = singleSwitches.Where(b => !b.Letter.HasValue && b.Word != null).ToList();
            if (singlesWithWords.Any())
                sb.Append($" [--{singlesWithWords.Select(x => x.Word).OrderBy(x => x).Join("|")} v1]");

            var valuesSwitches = parameters.OfType<ValuesSwitch>().ToList();
            var valuesWithLetters = valuesSwitches.Where(b => b.Letter.HasValue).GroupBy(b => b.MaxAllowed)
                .OrderBy(x => x.Key).ToList();
            foreach (var g in valuesWithLetters)
            {
                var valueList = "v1";

                if (g.Key > 1) valueList += g.Key == int.MaxValue ? $"..vN" : $"..v{g.Key}";

                var inner = g.Select(x => $"{x.Letter}").OrderBy(x => x);
                sb.Append($" [-{inner.Join("")} {valueList}]");
            }

            var valuesWithWords = valuesSwitches.Where(b => !b.Letter.HasValue && b.Word != null)
                .GroupBy(b => b.MaxAllowed).ToList();
            foreach (var g in valuesWithWords)
            {
                var valueList = "v1";

                if (g.Key > 1) valueList += g.Key == int.MaxValue ? $"..vN" : $"..v{g.Key}";

                sb.Append($" [--{g.Select(x => $"{x.Word}").OrderBy(x => x).Join("|")} {valueList}]");
            }

            var positionals = parameters.OfType<Positional>().ToList();
            foreach (var p in positionals)
            {
                var prefix = p.Help?.ValueAlias.IsNullOrWhiteSpace() ?? true ? "p" : $"{p.Help.ValueAlias}";
                var positionalList = $"{prefix}1";
                if (p.MaxAllowed > 1) positionalList += p.MaxAllowed == int.MaxValue ? $"..{prefix}N" : $"..{prefix}{p.MaxAllowed}";

                sb.Append($" [{positionalList}]");
            }

            return new TextNode(sb.ToString());
        }
    }
}