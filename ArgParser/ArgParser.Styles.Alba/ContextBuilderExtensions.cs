using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class AlbaSettings
    {
        protected internal Dictionary<string, Theme> ParserThemes { get; set; } = new Dictionary<string, Theme>();
    }

    public static class ContextBuilderExtensions
    {
        internal static Dictionary<ContextBuilder, AlbaSettings> Settings = new Dictionary<ContextBuilder, AlbaSettings>();

        public static ContextBuilder AddAlba(this ContextBuilder builder)
        {
            Settings.Add(builder, new AlbaSettings());
            return builder;
        }
    }

    public class AlbaBuilder
    {
        protected internal AlbaSettings AlbaSettings { get; set; }
        
        public AlbaBuilder WithParserTheme(string parserId, Theme theme)
        {
            AlbaSettings.ParserThemes.Add(parserId, theme);
            return this;
        }

        public ContextBuilder Finish { get; protected internal set; }
    }

    public class AlbaContext : IContext
    {
        public IContext Context { get; set; }

        public IHierarchyRepository HierarchyRepository => Context.HierarchyRepository;

        public IParserRepository ParserRepository => Context.ParserRepository;

        public Dictionary<string, Theme> ParserThemes { get; set; }
    }
}
