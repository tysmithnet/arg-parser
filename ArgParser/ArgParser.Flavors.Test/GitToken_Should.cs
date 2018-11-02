using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitToken_Should
    {
        [Fact]
        public void Have_The_Correct_Key()
        {
            // arrange
            var token = new GitToken("--something=other");

            // act
            // assert
            token.Key.Should().Be("something");
            token.Value.Should().Be("other");
        }

        [Fact]
        public void Return_The_Correct_Values()
        {
            // arrange
            var singleChar = new GitToken("-C");
            var word = new GitToken("--thing");
            var group = new GitToken("-abc");

            // act
            // assert
            singleChar.Raw.Should().Be("-C");
            singleChar.IsAnyMatch.Should().BeTrue();
            singleChar.Letter.Should().Be('C');

            word.Raw.Should().Be("--thing");
            word.IsAnyMatch.Should().BeTrue();
            word.Letter.Should().BeNull();
            word.Word.Should().Be("thing");
            word.Value.Should().BeNull();
        }
    }
}