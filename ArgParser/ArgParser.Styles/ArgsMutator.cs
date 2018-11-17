using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ArgsMutator : IArgsMutator
    {
        public ArgsMutator(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IContext Context { get; private set; }

        public string[] Mutate(MutateArgsRequest request)
        {
            var allSwitchesForChain = request.Chain.SelectMany(x => x.Parameters).OfType<Switch>().ToList();
            var booleanSwitches = allSwitchesForChain.OfType<BooleanSwitch>().ToList();
            var others = allSwitchesForChain.Except(booleanSwitches).ToList();
            var booleanLetters = booleanSwitches.Where(s => s.Letter.HasValue).Select(x => x.Letter.Value.ToString()).Join("");
            var otherLetters = others.Where(s => s.Letter.HasValue).Select(x => x.Letter.Value.ToString()).Join("");
            if (!booleanLetters.Any())
                return request.Args;
            List<string> groups;
            if (others.Any())
                groups = request.Args.Where(a => Regex.IsMatch(a, $"-[{booleanLetters}]+[{otherLetters}]?")).ToList();
            else
                groups = request.Args.Where(a => Regex.IsMatch(a, $"-[{booleanLetters}]+")).ToList();
            var copy = request.Args.ToList();
            foreach (var g in groups)
            {
                for (var i = 0; i < copy.Count; i++)
                {
                    var c = copy[i];
                    if (g.Contains(c))
                    {
                        copy.RemoveAt(i);
                        var letters = c.Substring(1).ToCharArray().Reverse();
                        foreach (var letter in letters)
                        {
                            copy.Insert(i, $"-{letter}");
                        }
                    }
                }
            }

            return copy.ToArray();
        }
    }
}