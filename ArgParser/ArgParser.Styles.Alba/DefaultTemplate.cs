using System.IO;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class DefaultTemplate : Template
    {
        public string ParserId { get; set; }
        
        public DefaultTemplate(IContext context, string parserId) : base(context)
        {
            ParserId = parserId.ThrowIfArgumentNull(nameof(parserId));
        }

        public override Document Create()
        {
            var vm = new DefaultTemplateVm()
            {
                Title = ParserId,
            };

            using (var fs = File.OpenRead("Views/DefaultTemplate.xaml"))
            {
                return ConsoleRenderer.ReadDocumentFromStream(fs, this, new XamlElementReaderSettings()
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