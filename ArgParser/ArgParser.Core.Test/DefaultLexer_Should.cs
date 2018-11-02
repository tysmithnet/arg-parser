using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultLexer_Should
    {
        private class NumberToken : IToken
        {
            public int Number { get; set; }

            public string Raw => Number.ToString();
        }

        [Fact]
        public void Simply_Return_Basic_Tokens()
        {
            // arrange
            var args = new[] {"arg1", "arg2", "arg3"};
            var lexer = new DefaultLexer();

            // act
            // assert
            lexer.Lex(args).Should().BeEquivalentTo(args.Select(a => new DefaultToken(a)));
        }

        [Fact]
        public void Simply_Return_Basic_Tokens_Generic()
        {
            // arrange
            var args = new[] {"arg1", "arg2", "arg3"};
            var lexer = new DefaultLexer<NumberToken>(s => new NumberToken
            {
                Number = 1
            });

            // act
            // assert
            lexer.Lex(args).All(t => t.Number == 1).Should().BeTrue();
        }
    }
}