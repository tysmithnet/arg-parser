using System;
using System.IO;
using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public abstract class Template
    {
        protected internal string FileName { get; set; }
        public static readonly Template Default = new DefaultTemplate();

        public static Template FromString(string template)
        {
            return new StringTemplate(template);
        }

        public virtual Stream GetTemplateStream()
        {
            return File.OpenRead($"Views/{FileName}");
        }

        private class DefaultTemplate : Template
        {
            public DefaultTemplate()
            {
                FileName = "Default.xaml";
            }
        }

        private class StringTemplate : Template
        {
            string Template { get; set; }

            public StringTemplate(string template)
            {
                Template = template ?? throw new ArgumentNullException(nameof(template));
            }

            public override Stream GetTemplateStream()
            {
                return Template.ToStream();
            }
        }
        
    }
}