using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Alba.CsConsoleFormat;

namespace ArgParser.Styles.Alba
{
    public class ElementFactory : IElementFactory
    {
        public TElement InflateTempalte<TElement>(string relativePath, object vm, params Assembly[] assemblies) where TElement : Element, new()
        {
            using (var fs = File.OpenRead(relativePath))
            {
                var settings = new XamlElementReaderSettings();
                foreach (var assembly in assemblies)
                {
                    settings.ReferenceAssemblies.Add(assembly);
                }

                return ConsoleRenderer.ReadElementFromStream<TElement>(fs, vm, settings);
            }
        }
    }
}
