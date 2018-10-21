using System.Linq;

namespace ArgParser.Core
{
    public class DefaultIterationInfoFactory : IIterationInfoFactory
    {
        protected internal ILexer Lexer { get; set; } = new DefaultLexer();

        /// <inheritdoc />
        public IIterationInfo Create(string[] args)
        {
            var tokens = Lexer.Lex(args);
            return new IterationInfo()
            {
                Tokens = tokens.ToList(),
                Index = 0,
                Args = args.ToArray()
            };
        }
    }
}