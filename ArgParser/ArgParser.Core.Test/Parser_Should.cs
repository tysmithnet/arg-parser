using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Core.Test
{
    public class Parser_Should
    {
        [Fact]
        public void Consume_Using_Only_The_First_Parameter_That_Can_Consume()
        {
            // arrange
            var parser = new Parser("base");
            var info = new IterationInfo("a b c".Split(' '));

            var mock0 = new Mock<Parameter>();
            mock0.SetupAllProperties();
            mock0.Setup(p => p.CanConsume(It.IsAny<object>(), It.IsAny<IterationInfo>())).Returns(new
                ConsumptionResult(info.Consume(1), 1, mock0.Object));
            mock0.Setup(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>()))
                .Returns(new ConsumptionResult(info.Consume(1), 1, mock0.Object));
            parser.AddParameter(mock0.Object);

            var mock1 = new Mock<Parameter>();
            mock1.SetupAllProperties();
            mock1.Setup(p => p.CanConsume(It.IsAny<object>(), It.IsAny<IterationInfo>()))
                .Returns(new ConsumptionResult(info.Consume(1), 1, mock1.Object));
            mock1.Setup(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>()))
                .Returns(new ConsumptionResult(info.Consume(1), 1, mock1.Object));
            parser.AddParameter(mock1.Object);

            // act
            // assert
            parser.Consume("", new ConsumptionRequest(info));
            mock0.Verify(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>()), Times.Once);
            mock1.Verify(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>()), Times.Never);
        }

        [Fact]
        public void Indicate_It_Can_Consume_When_One_Of_Its_Parameters_Can_Consume()
        {
            // arrange
            var parser = new Parser("base");
            var info = new IterationInfo("a b c".Split(' '));
            var mock = new Mock<Parameter>();
            mock.Setup(p => p.CanConsume(It.IsAny<object>(), info)).Returns(new ConsumptionResult(info.Consume(1), 1, mock.Object));
            parser.AddParameter(mock.Object);

            // act
            // assert
            parser.CanConsume(new object(), info).NumConsumed.Should().Be(1);
        }

        [Fact]
        public void Provide_A_Generic_Version()
        {
            // arrange
            var p = new Parser<string>("base");
            p.FactoryFunction = () => "";

            // act
            // assert
            p.FactoryFunction().Should().Be("");
        }
    }
}