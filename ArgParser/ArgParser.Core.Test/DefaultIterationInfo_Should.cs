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
    }
}