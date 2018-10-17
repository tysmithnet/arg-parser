using System;

namespace ArgParser.Core
{
    public class SubCommand<T>
    {
        public Func<string, bool> IsCommand { get; set; }
        public ArgParser<T> ArgParser { get; set; }
    }
}