using System;
using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles.Alba
{
    public class AlbaContext : IContext
    {
        public AlbaContext(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;
        public IParserRepository ParserRepository => Context.ParserRepository;
        public IThemeRepository ThemeRepository { get; protected internal set; } = new ThemeRepository();
        protected internal IContext Context { get; set; }
    }
}