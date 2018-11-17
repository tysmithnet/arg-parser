using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Styles.Alba
{
    public static class ContextBuilderExtensions
    {
        internal static Dictionary<ContextBuilder, AlbaSettings> Settings = new Dictionary<ContextBuilder, AlbaSettings>();

        public static ContextBuilder AddAlba(this ContextBuilder builder)
        {
            Settings.Add(builder, new AlbaSettings());
            return builder;
        }
    }
}
