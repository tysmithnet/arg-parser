using System;

namespace ArgParser.Core
{
    public interface ISubCommand
    {
        Func<IterationInfo, bool> IsCommand { get; set; }
        ParseResult Parse(string[] args);
    }
}