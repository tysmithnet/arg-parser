using System;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AlbaContext : IContext
    {
        protected internal IContext Context { get; set; }
        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;
        public IParserRepository ParserRepository => Context.ParserRepository;

        public AlbaContext(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}