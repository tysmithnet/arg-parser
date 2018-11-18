﻿using System;
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
        public IThemeRepository ThemeRepository { get; protected internal set; } = new ThemeRepository();
    }
}