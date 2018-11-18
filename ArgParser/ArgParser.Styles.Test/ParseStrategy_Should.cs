using System;
using System.Linq;
using ArgParser.Core;
using ArgParser.Testing.Common;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class ParseStrategy_Should
    {
        [Fact]
        public void Set_Sub_Component_Contexts_When_The_Context_Changes()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var strat = new ParseStrategy(builder.Context, "util");
            var newGuy = new Context();

            // act
            strat.Context = newGuy;

            // assert
            strat.Context.Should().BeSameAs(newGuy);
            strat.ArgsMutator.Context.Should().BeSameAs(newGuy);
            strat.ChainIdentificationStrategy.Context.Should().BeSameAs(newGuy);
            strat.ConsumerSelectionStrategy.Context.Should().BeSameAs(newGuy);
            strat.ConsumptionRequestFactory.Context.Should().BeSameAs(newGuy);
            strat.IterationInfoFactory.Context.Should().BeSameAs(newGuy);
            strat.ParseResultFactory.Context.Should().BeSameAs(newGuy);
            strat.PotentialConsumerStrategy.Context.Should().BeSameAs(newGuy);
        }

        [Fact]
        public void Not_Throw_If_An_Unexpected_Argument_Is_Found()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var strat = new ParseStrategy(builder.Context, "util");
            var mock = new Mock<IPotentialConsumerStrategy>();
            mock.SetupAllProperties();
            mock.Setup(s => s.IdentifyPotentialConsumer(It.IsAny<PotentialConsumerRequest>())).Returns(
                new PotentialConsumerResult(builder.Context.PathToRoot("util"), new ConsumptionResult[0],
                    new IterationInfo("-h".Split(' '))));
            strat.PotentialConsumerStrategy = mock.Object;
            IParseResult res = null;
            Action mightThrow = () => res = strat.Parse("-h".Split(' '), builder.Context);

            // act
            // assert
            mightThrow.Should().NotThrow();
            bool isParsed = false;
            res.WhenError(exceptions =>
            {
                exceptions.Single().Should().BeOfType<UnexpectedArgException>();
                isParsed = true;
            });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Not_Throw_If_No_Factory_Function_Set()
        {
            // arrange
            var builder = new ContextBuilder("root")
                .AddParser("root")
                .Finish;
            var strat = new ParseStrategy(builder.Context, "root");
            IParseResult res = null;
            Action mightThrow = () => res = strat.Parse("-h".Split(' '), builder.Context);

            // act
            // assert
            mightThrow.Should().NotThrow();
            bool isParsed = false;
            res.WhenError(exceptions =>
            {
                isParsed = true;
                exceptions.Single().Should().BeOfType<NoFactoryFunctionException>();
            });
            isParsed.Should().BeTrue();
        }
    }
}