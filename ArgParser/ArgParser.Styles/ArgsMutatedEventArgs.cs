using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ArgsMutatedEventArgs : ParserEventArgs
    {
        public string[] Original { get; protected internal set; }
        public string[] Mutated { get; protected internal set; }

        public ArgsMutatedEventArgs(string[] original, string[] mutated, IContext context) : base(context)
        {
            Original = original.ThrowIfArgumentNull(nameof(original));
            Mutated = mutated.ThrowIfArgumentNull(nameof(mutated));
            Context = context.ThrowIfArgumentNull(nameof(context));
        }
    }
}