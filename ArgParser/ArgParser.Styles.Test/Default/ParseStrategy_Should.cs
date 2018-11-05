using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ParseStrategy_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow = () => new ParseStrategy(null);

            // act
            // assert
            mightThrow.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Limit_A_Switches_Consumption_If_It_Tries_To_Consume_Another_Switch()
        {
            // arrange
            var strat = new ParseStrategy("base");
            var context = new Context();
            var parser = context.ParserRepository.Create("base");
            parser.AddParameter(new BooleanSwitch('h', "help", o => {}));
            parser.AddParameter(new BooleanSwitch('v', "verbose", o => { }));
            parser.AddParameter(new ValuesSwitch('d', "data", (o, strings) => { }));
            var info = new IterationInfo("-d d0 d1 d2 d3 -v -h".Split(' '));
            var result = new ConsumptionResult(info, 7);

            // act
            var request = strat.CreateCanConsumeRequest("", parser.ToEnumerableOfOne().ToList(), info, result);

            // assert
            request.Max.Should().Be(5);
            request.Info.Should().BeSameAs(info);
        }

        [Fact]
        public void Allow_A_Switch_To_Be_Greedy_When_No_Conflicting_Switch_Values()
        {
            // arrange
            var strat = new ParseStrategy("base");
            var context = new Context();
            var parser = context.ParserRepository.Create("base");
            parser.AddParameter(new BooleanSwitch(null, "help", o => { }));
            parser.AddParameter(new BooleanSwitch(null, "verbose", o => { }));
            parser.AddParameter(new ValuesSwitch('d', "data", (o, strings) => { }));
            var info = new IterationInfo("-d d0 d1 d2 d3 -v -h".Split(' '));
            var result = new ConsumptionResult(info, 7);

            // act
            var request = strat.CreateCanConsumeRequest("", parser.ToEnumerableOfOne().ToList(), info, result);

            // assert
            request.Max.Should().Be(7);
            request.Info.Should().BeSameAs(info);
        }

        [Fact]
        public void Get_The_Correct_Parser_Tree()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Build()
                .AddParser("child")
                .Build()
                .AddParser("gchild")
                .Build()
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");
            var gchild = context.ParserRepository.Get("gchild");
            var child = context.ParserRepository.Get("child");
            var baze = context.ParserRepository.Get("base");

            // act
            var chain = strat.GetParserFamily(context, gchild);

            // assert
            chain.Should().BeEquivalentTo(new[] {gchild, child, baze});
        }

        [Fact]
        public void Identify_The_Correct_Parser_Based_Off_Which_Commands_Are_Children()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Build()
                .AddParser("child")
                .Build()
                .AddParser("gchild")
                .Build()
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");
            var gchild = context.ParserRepository.Get("gchild");
            var child = context.ParserRepository.Get("child");
            var baze = context.ParserRepository.Get("base");

            // act
            var identifiedParser = strat.IdentifyRelevantParser("child gchild".Split(' '), context);

            // assert
            identifiedParser.Should().BeSameAs(gchild);
        }
    }
}
