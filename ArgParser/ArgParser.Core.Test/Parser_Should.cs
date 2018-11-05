using System;
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

        [Fact]
        public void Consume_Using_Only_The_First_Parameter_That_Can_Consume()
        {
            // arrange
            var parser = new Parser("base");
            var info = new IterationInfo("a b c".Split(' '));

            var mock0 = new Mock<Parameter>();
            mock0.SetupAllProperties();
            mock0.Setup(p => p.CanConsume(It.IsAny<object>(), It.IsAny<IterationInfo>())).Returns(new 
                ConsumptionResult(info.Consume(1), 1));
            mock0.Setup(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>())).Returns(new ConsumptionResult(info.Consume(1), 1));
            parser.AddParameter(mock0.Object);

            var mock1 = new Mock<Parameter>();
            mock1.SetupAllProperties();
            mock1.Setup(p => p.CanConsume(It.IsAny<object>(), It.IsAny<IterationInfo>())).Returns(new ConsumptionResult(info.Consume(1), 1));
            mock1.Setup(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>())).Returns(new ConsumptionResult(info.Consume(1), 1));
            parser.AddParameter(mock1.Object);

            // act
            // assert
            parser.Consume("", new ConsumptionRequest(info));
            mock0.Verify(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>()), Times.Once);
            mock1.Verify(p => p.Consume(It.IsAny<object>(), It.IsAny<ConsumptionRequest>()), Times.Never);
        }

        [Fact]
        public void Not_Allow_The_Same_Factory_Funcion_To_Be_Added_More_Than_Once()
        {
            // arrange
            var parser = new Parser("id");
            Func<object> fac = () => "";

            // act
            parser.AddFactoryFunction(fac);
            parser.AddFactoryFunction(fac);

            // assert
            parser.FactoryFunctionsInternal.Should().OnlyContain(func => func == fac);
            parser.FactoryFunctionsInternal.Should().HaveCount(1);
            parser.FactoryFunctions.Should().OnlyContain(func => func == fac);
            parser.FactoryFunctions.Should().HaveCount(1);
        }
    }
}