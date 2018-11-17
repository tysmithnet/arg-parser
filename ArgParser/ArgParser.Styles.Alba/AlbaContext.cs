﻿using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AlbaContext : IContext
    {
        public AlbaContext(IContext context)
        {
            Context = context.ThrowIfArgumentNull(nameof(context));
        }

        public IContext Context { get; set; }

        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;

        public IParserRepository ParserRepository => Context.ParserRepository;

        public Dictionary<string, Theme> ParserThemes { get; set; } = new Dictionary<string, Theme>();
    }
}