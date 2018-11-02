using System.Collections.Generic;

namespace ArgParser.IntegrationTests.Options.Trivial
{
    public class TestOptions
    {
        public string ConfigFile { get; set; }
        public List<string> TestConfigurations { get; set; } = new List<string>();
    }
}