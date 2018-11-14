using System.IO;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplate : Template
    {
        public DefaultTemplate(IContext context) : base(context)
        {
        }

        public override Document Create()
        {
            using (var fs = File.OpenRead("Views/DefaultTemplate.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, new Parser("util"), new XamlElementReaderSettings()
                {
                    ReferenceAssemblies =
                    {
                        typeof(DefaultTemplate).Assembly
                    }
                });
            }
        }
    }
}