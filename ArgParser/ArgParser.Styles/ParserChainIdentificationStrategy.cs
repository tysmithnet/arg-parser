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

        public IContext Context { get; set; }

        public ChainIdentificationResult Identify(ChainIdentificationRequest request)
        {
            var ids = new List<string>();
            var rootId = request.Context.HierarchyRepository.GetRoot();
            for (var i = 0; i < request.Args.Length; i++)
            {
                var left = i == 0 ? rootId : request.Args[i - 1];
                var right = request.Args[i];
                if (request.Context.HierarchyRepository.IsParent(left, right)) ids.Add(right);
                else break;
            }

            var chain = ids.Any() ? Context.PathToRoot(ids.Last()).Reverse().ToList() : request.Context.ParserRepository.Get(request.Context.HierarchyRepository.GetRoot()).ToEnumerableOfOne().ToList();
            
            var res = new ChainIdentificationResult()
            {
                Chain = chain,
                IdentifiedParser = chain.Last(),
                ConsumedArgs = request.Args.Take(ids.Count).ToArray()
            };
            return res;
        }
    }
}