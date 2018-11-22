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
            var builder = new ContextBuilder()
                .AddParser("a")
                .WithAlias("1")
                .Finish;
            var context = new AlbaContext(builder.Context);

            // act
            // assert
            context.AliasRepository.HasAlias("a").Should().BeTrue();
        }
    }
}