using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Styles.ParseStrategy;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ConsumerSelectionStrategy_Should
    {
        [Fact]
        public void Throw_If_Cannot_Find_Parser()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var strat = new ConsumerSelectionStrategy(builder.Context);
            var info = new IterationInfo("firewall -h".Split(' '));
            var parameter = new BooleanSwitch(new Parser("fake"), 'h', "help", o => { });
            var result = new PotentialConsumerResult()
            {
                Chain = builder.Context.PathToRoot("firewall").Reverse().ToList(),
                Info = info,
                ConsumptionResults = new []
                {
                    new ConsumptionResult()
                    {
                        ConsumingParameter = parameter,
                        Info = info,
                        ParseExceptions = null
                    }
                }
            };
            Action mightThrow = () => strat.Select(result);

            // act
            // assert
            mightThrow.Should().Throw<ForwardProgressException>();
        }
    }
}
