using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class SeparatedSwitch_Should
    {
        [Fact]
        public void Consume_Everything_After_The_Separator_As_A_Single_Value0()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var root = builder.Context.ParserRepository.Get("util");
            string value = null;
            var sut = new SeparatedSwitch(root, 'v', "value", (o, s) => { value = s; });

            // act
            var info = new IterationInfo("-v=a".Split(' '));
            var res = sut.Consume("", new ConsumptionRequest(info));

            // assert
            res.ConsumingParameter.Should().BeSameAs(sut);
            value.Should().Be("a");
        }

        [Fact]
        public void Consume_Everything_After_The_Separator_As_A_Single_Value1()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var root = builder.Context.ParserRepository.Get("util");
            string value = null;
            var sut = new SeparatedSwitch(root, 'v', "value", (o, s) => { value = s; })
            {
                Separator = ":"
            };
            var info = new IterationInfo("--value:a".Split(' '));

            // act
            // assert
            var canConsumeResult = sut.CanConsume("", info);
            canConsumeResult.ConsumingParameter.Should().BeSameAs(sut);
            canConsumeResult.NumConsumed.Should().Be(1);
            canConsumeResult.ParseExceptions.Should().BeEmpty();
            canConsumeResult.Info.Index.Should().Be(1);

            var consumeResult = sut.Consume("", new ConsumptionRequest(info));
            consumeResult.ConsumingParameter.Should().BeSameAs(sut);
            consumeResult.NumConsumed.Should().Be(1);
            consumeResult.ParseExceptions.Should().BeEmpty();
            consumeResult.Info.Index.Should().Be(1);
        }

        [Fact]
        public void Work_With_Letters()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var root = builder.Context.ParserRepository.Get("util");
            string value = null;
            var sut = new SeparatedSwitch(root, 'v', "value", (o, s) => { value = s; })
            {
                Separator = ":"
            };
            var info = new IterationInfo("-v:a".Split(' '));

            // act
            // assert
            var canConsumeResult = sut.CanConsume("", info);
            canConsumeResult.ConsumingParameter.Should().BeSameAs(sut);
            canConsumeResult.NumConsumed.Should().Be(1);
            canConsumeResult.ParseExceptions.Should().BeEmpty();
            canConsumeResult.Info.Index.Should().Be(1);

            var consumeResult = sut.Consume("", new ConsumptionRequest(info));
            consumeResult.ConsumingParameter.Should().BeSameAs(sut);
            consumeResult.NumConsumed.Should().Be(1);
            consumeResult.ParseExceptions.Should().BeEmpty();
            consumeResult.Info.Index.Should().Be(1);
        }

        [Fact]
        public void Return_No_Consumption_Result_If_Doesnt_Match()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var root = builder.Context.ParserRepository.Get("util");
            string value = null;
            var sut = new SeparatedSwitch(root, 'v', "value", (o, s) => { value = s; })
            {
                Separator = ":"
            };
            var info = new IterationInfo("-x:a".Split(' '));

            // act
            // assert
            var res = sut.CanConsume("", info);
            res.ConsumingParameter.Should().Be(sut);
            res.NumConsumed.Should().Be(0);
            res.ParseExceptions.Should().BeEmpty();
        }
    }
}
