using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultIterationInfo_Should
    {
        [Fact]
        public void Return_The_Token_At_Index_For_Current()
        {
            // arrange
            var info = new DefaultIterationInfo();
            info = (DefaultIterationInfo) info.SetTokens(new List<IToken>
            {
                new Token("a"),
                new Token("b"),
                new Token("c")
            });

            // act
            // assert
            info.Current.Should().Be(new Token("a"));
            info.Index = 1;
            info.Current.Should().Be(new Token("b"));
            info.Index = 2;
            info.Current.Should().Be(new Token("c"));
        }

        [Fact]
        public void Consume_The_Correct_Amount_Of_Tokens()
        {
            var info = new DefaultIterationInfo();
            info = (DefaultIterationInfo)info.SetTokens(new List<IToken>
            {
                new Token("a"),
                new Token("b"),
                new Token("c"),
                new Token("d"),
                new Token("e"),
                new Token("f"),
            });

            // act
            // assert
            info.Index.Should().Be(0);
            info.Consume(0).Index.Should().Be(0);
            info.Consume(1).Index.Should().Be(1);
            info.Consume(1).Current.Raw.Should().Be("b");
            info.Consume(2).Index.Should().Be(2);
            info.Consume(2).Current.Raw.Should().Be("c");
            info.Consume(3).Consume(1).Index.Should().Be(4);
            info.Consume(3).Consume(1).Current.Raw.Should().Be("e");
            info.Consume(3).Consume(2).Index.Should().Be(5);
            info.Consume(3).Consume(2).Current.Raw.Should().Be("f");
        }
    }
}