using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using Colorful;

namespace ArgParser.Styles.Help.Alba
{
    public static class HelpExtensions
    {
        internal static Dictionary<ContextBuilder, HelpRenderer> Renderers { get; set; } = new Dictionary<ContextBuilder, HelpRenderer>();
        internal static Dictionary<Parser, Theme> Themes { get; set; } = new Dictionary<Parser, Theme>();

        public static void RenderHelp(this ContextBuilder contextBuilder, string parserId, int width = 80)
        {
            using (var fs = File.OpenRead("Views/Default.xaml"))
            {
                var doc = ConsoleRenderer.ReadDocumentFromStream(fs, new Something());
                ConsoleRenderer.RenderDocument(doc);
            }
        }

        public static ContextBuilder AddRenderSupport(this ContextBuilder builder)
        {
            if(!Renderers.ContainsKey(builder))
                Renderers.Add(builder, new HelpRenderer());
            return builder;
        }

        public static ContextBuilder AddMultiColoredCommands(this ContextBuilder builder, params Color[] colors)
        {
            var convertedColors = colors.PreventNull().Select(c => c.ToNearestConsoleColor()).ToArray();
            var renderer = Renderers[builder];
            
            return builder;
        }
    }
    public class Something
    {
        public IEnumerable<Person> People { get; set; } = new List<Person>()
        {
            new Person()
            {
                Name = "Alice",
                Age = 64
            },
            new Person()
            {
                Name = "Bob",
                Age = 24
            }
        };
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}