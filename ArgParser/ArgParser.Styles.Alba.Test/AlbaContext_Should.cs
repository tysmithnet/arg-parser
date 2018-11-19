using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class AlbaContext_Should
    {
        [Fact]
        public void Proxy_For_The_Inner_Context()
        {
            // arrange
            var inner = new Context();
            inner.AliasRepository.SetAlias("a", "1");
            var context = new AlbaContext(inner);

            // act
            // assert
            context.AliasRepository.HasAlias("a").Should().BeTrue();

        }
    }
}