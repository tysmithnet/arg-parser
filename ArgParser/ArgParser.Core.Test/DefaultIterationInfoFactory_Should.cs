using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultIterationInfoFactory_Should
    {
        private class UppercaseLexer : ILexer
        {
            public IEnumerable<IToken> Lex(string[] args)
            {
                return args.Select(x => x.ToUpper()).Select(x => new DefaultToken(x));
            }
        }

        [Fact]
        public void Allow_For_Custom_Lexers()
        {
            // arrange
            var lexer = new UppercaseLexer();
            var fac = new DefaultIterationInfoFactory
            {
                Lexer = lexer
            };

            // act
            var info = fac.Create(new[] {"a", "b"});

            // assert
            fac.Lexer = lexer;
            info.Tokens.Should().BeEquivalentTo(new DefaultToken("A"), new DefaultToken("B"));
        }

        [Fact]
        public void Create_Default_Info()
        {
            // arrange
            var fac = new DefaultIterationInfoFactory();

            // act
            var info = fac.Create(new[] {"a", "b"});

            // assert
            info.Tokens.Should().HaveCount(2);
            info.Current.Raw.Should().Be("a");
            info.Index.Should().Be(0);
            info.IsComplete.Should().BeFalse();
            info.Next.Raw.Should().Be("b");
            info.Rest.Should().HaveCount(1);
        }
    }
}