using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Styles.Default.Help;

namespace ArgParser.Styles.Default
{
    public class ParserHelp : FullHelp
    {
        public string Version { get; set; }
        public string Author { get; set; }
        public string RepositoryUrl { get; set; }
    }
}
