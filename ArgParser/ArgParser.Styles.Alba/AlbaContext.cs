using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AlbaContext : IContext
    {
        public AlbaContext(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;
        public IParserRepository ParserRepository => Context.ParserRepository;
        protected internal IContext Context { get; set; }
        protected internal Dictionary<Parser, Theme> Themes { get; set; } = new Dictionary<Parser, Theme>();
    }
}