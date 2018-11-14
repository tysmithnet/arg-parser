using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alba.CsConsoleFormat;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class DefaultViewModel : IViewModel
    {
        public IContext Context { get; protected internal set; }

        public void Setup()
        {
        }

        public Document Create()
        {
            using (var fs = File.OpenRead("Views/Default.xaml"))
            {
                var element = ConsoleRenderer.ReadElementFromStream<Div>(fs, new Parser("hi"));
                var document = new Document(element);
                return document;
            }
        }
    }
}
