using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class ParseException_Should
    {
        [Fact]
        public void Require_A_Message()
        {
            // arrange
            var ex = new ParseException("test message");

            // act
            // assert
            ex.Message.Should().Be("test message");
        }
    }
}