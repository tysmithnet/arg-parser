using System;

namespace ArgParser.Core
{
    public class SubCommand<T> : ISubCommand
    {
        internal ArgParser<T> ArgParser { get; set; }
        public ParseResult Parse(string[] args)
        {
            return ArgParser.Parse(args);
        }

        /// <inheritdoc />
        public Func<IterationInfo, bool> IsCommand { get; set; }
    }
}