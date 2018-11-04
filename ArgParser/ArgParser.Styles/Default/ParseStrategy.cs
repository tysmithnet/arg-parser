using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParseStrategy : IParseStrategy
    {
        public IParseResult Parse(string[] args, IContext context)
        {
            // todo: arg check

            var parsers = new List<Parser>();
            
            // find last sub command
            int i = 0;
            for (; i < args.Length; i++)
            {
                string left = i == 0 ? null : args[i - 1];
                string right = args[i];
                if (!context.HierarchyRepository.IsParent(left, right))
                    break;
                else
                    parsers.Add(context.HierarchyRepository.Get(right));
            }

            var factoryFuncs = parsers.SelectMany(x => x.FactoryFunctions);

            foreach (var fac in factoryFuncs)
            {
                var instance = fac();
                var info = new IterationInfo(args, 0);
                foreach (var parser in parsers)
                {
                    
                }
            }

            return null;
        }
    }
}
