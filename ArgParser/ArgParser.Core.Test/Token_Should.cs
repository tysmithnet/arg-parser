using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class Token_Should
    {
        [Fact]
        public void Exhibit_Value_Equality()
        {
            // arrange
            var a = new DefaultToken("a");
            var b = new DefaultToken("a");
            var c = new DefaultToken("c");

            // act
            // assert
            a.GetHashCode().Should().Be(b.GetHashCode());
            a.GetHashCode().Should().NotBe(c.GetHashCode());

            a.Equals(a).Should().BeTrue();
            a.Equals(b).Should().BeTrue();
            a.Equals(c).Should().BeFalse();
            a.Equals(null).Should().BeFalse();
            a.Equals("").Should().BeFalse();
            a.Equals((object) a).Should().BeTrue();
            a.Equals((object) b).Should().BeTrue();
            a.Equals((object) c).Should().BeFalse();
            a.Equals((object) null).Should().BeFalse();
        }
    }
}