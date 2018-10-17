using System;

namespace ArgParser.Core
{
    public class SubCommand<T, TParent> : ISubCommand where T : TParent
    {
        internal SubCommandArgParser<T, TParent> ArgParser { get; set; }
        public ParseResult Parse(string[] args)
        {
            return ArgParser.Parse(args);
        }

        /// <inheritdoc />
        public Func<IterationInfo, bool> IsCommand { get; set; }
    }
}