using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class XamlTemplate<TModel> : Template<TModel> where TModel : IViewModel
    {
        protected internal string ViewPath { get; set; }
        protected internal List<Assembly> Assemblies { get; set; }

        public XamlTemplate(string viewPath, params Assembly[] assemblies)
        {
            ViewPath = viewPath ?? throw new ArgumentNullException(nameof(viewPath));
            Assemblies = assemblies.PreventNull().ToList();
        }

        public override TElement Inflate<TElement>(TModel model)
        {
            using (var fs = File.OpenRead(ViewPath))
            {
                var xamlSettings = new XamlElementReaderSettings();
                Assemblies.ForEach(a => xamlSettings.ReferenceAssemblies.Add(a));
                return ConsoleRenderer.ReadElementFromStream<TElement>(fs, model, xamlSettings);
            }
        }
    }
}
