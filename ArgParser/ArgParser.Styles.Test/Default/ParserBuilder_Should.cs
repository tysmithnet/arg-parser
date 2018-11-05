using System.Linq;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ParserBuilder_Should
    {
        [Fact]
        public void Allow_Boolean_Switches_To_Be_Added()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser = new Parser("base");
            var builder = new ParserBuilder(contextBuilder, parser);
            var parseCount = 0;

            // act
            builder.WithBooleanSwitch('h', "help", o => parseCount++);

            // assert
            var result = (BooleanSwitch) parser.Parameters.Single();
            result.Letter.Should().Be('h');
            result.Word.Should().Be("help");
        }

        [Fact]
        public void Return_The_Same_ContextBuilder_It_Was_Created_With()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser = new Parser("base");
            var builder = new ParserBuilder(contextBuilder, parser);

            // act
            // assert
            builder.Finish.Should().BeSameAs(contextBuilder);
        }
    }
}