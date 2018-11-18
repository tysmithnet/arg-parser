using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AlbaContext : IContext
    {
        protected internal IContext Context { get; set; }
        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;
        public IParserRepository ParserRepository => Context.ParserRepository;
        protected internal Dictionary<Parser, Theme> Themes { get; set; } = new Dictionary<Parser, Theme>();
        
        public AlbaContext(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}