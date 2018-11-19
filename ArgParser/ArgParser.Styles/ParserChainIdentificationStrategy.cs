using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParserChainIdentificationStrategy : IParserChainIdentificationStrategy
    {
        public ParserChainIdentificationStrategy(IContext context)
        {
            Context = context;
        }

        public ChainIdentificationResult Identify(ChainIdentificationRequest request)
        {
            var args = request.ThrowIfArgumentNull(nameof(request)).Args.PreventNull().ToArray();
            if(!args.Any())
                return new ChainIdentificationResult(Context.RootParser().ToEnumerableOfOne(), new string[0]);

            List<string> Helper(List<string> history)
            {
                var index = history.Count;
                if (index >= args.Length)
                    return history;
                
                var cur = args[index];
                var potentials = cur.ToEnumerableOfOne().Concat(Context.AliasRepository.Lookup(cur));
                var results = new List<List<string>>();
                foreach (var potential in potentials)
                {
                    var left = history.Any() ? history.Last() : Context.HierarchyRepository.GetRoot();
                    var right = potential;
                    if (!Context.HierarchyRepository.IsParent(left, right)) continue;
                    var copy = history.ToList();
                    copy.Add(right);
                    results.Add(Helper(copy));
                }

                if (!results.Any())
                    return history.ToList();

                var bestMatch = results.GroupBy(x => x.Count).OrderByDescending(x => x.Key).First();
                if(bestMatch.Count() > 1)
                    throw new AmbiguousCommandChainException(bestMatch.Select(x => x.ToList()).ToList());
                return bestMatch.Single().ToList();
            }

            var found = Helper(new List<string>());
            if (!found.Any())
                return new ChainIdentificationResult(Context.RootParser().ToEnumerableOfOne(), new string[0]);
            var chain = Context.PathToRoot(found.Last()).Reverse().ToList();
            return new ChainIdentificationResult(chain, args.Take(chain.Count - 1).ToArray());
        }

        public IContext Context { get; set; }
    }
}