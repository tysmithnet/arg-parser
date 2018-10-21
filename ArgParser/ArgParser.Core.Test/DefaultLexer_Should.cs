using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultLexer_Should
    {
        [Fact]
        public void Simply_Return_Basic_Tokens()
        {
            // arrange
            var args = new[] {"arg1", "arg2", "arg3"};
            var lexer = new DefaultLexer();

            // act
            // assert
            lexer.Lex(args).Should().BeEquivalentTo(args.Select(a => new Token(a)));
        }
    }
}