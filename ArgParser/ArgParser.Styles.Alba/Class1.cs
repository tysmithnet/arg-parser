using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.ColorfulConsole;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public abstract class Template
    {
        protected internal IContext Context { get; set; }

        protected Template(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract Document Create();
    }

    public class DefaultTemplate : Template
    {
        public DefaultTemplate(IContext context) : base(context)
        {
        }

        public override Document Create()
        {
            var banner = new AsciiArtBanner()
            {
                Title = "Hello",
                SubTitle = "World, hello world."
            };
            var doc = new Document(banner);
            return doc;
        }
    }

    public class AsciiArtBanner : BlockElement
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public override IEnumerable<Element> GenerateVisualElements()
        {
            var visualChildren = new List<Element>();
            using (var fs = File.OpenRead("Views/AsciiArtBanner.xaml"))
            {
                var element = ConsoleRenderer.ReadElementFromStream<Div>(fs, this, new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(FigletDiv).Assembly,
                        typeof(AsciiArtBanner).Assembly
                    }
                });
                visualChildren.Add(element);
            }

            return visualChildren;
        }
    }
}
