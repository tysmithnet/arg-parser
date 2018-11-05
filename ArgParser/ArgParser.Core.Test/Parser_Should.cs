using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Core.Test
{
    public class Parser_Should
    {
        [Fact]
        public void Indicate_It_Can_Consume_When_One_Of_Its_Parameters_Can_Consume()
        {
            // arrange
            var parser = new Parser("base");
            var info = new IterationInfo("a b c".Split(' '));
            var mock = new Mock<Parameter>();
            mock.Setup(p => p.CanConsume(It.IsAny<object>(), info)).Returns(new ConsumptionResult(info.Consume(1), 1));
            parser.AddParameter(mock.Object);

            // act
            // assert
            parser.CanConsume(new object(), info).NumConsumed.Should().Be(1);
        }
    }
}