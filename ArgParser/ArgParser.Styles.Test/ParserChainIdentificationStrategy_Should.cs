using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class ParserChainIdentificationStrategy_Should
    {
        [Fact]
        public void Allow_Aliases_To_Be_Used()
        {
            // arrange
            var builder = new ContextBuilder("a")
                .AddParser("a")
                .Finish
                .AddParser("b")
                .Finish
                .AddParser("c")
                .WithAlias("C")
                .Finish
                .CreateParentChildRelationship("a", "b")
                .CreateParentChildRelationship("b", "c");

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("b C".Split(' '), builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Should().HaveCount(3);
        }

        [Fact]
        public void Find_The_Correct_Alias_If_Multiple_Exist()
        {
            // arrange
            var builder = new ContextBuilder("a")
                .AddParser("a")
                .Finish
                .AddParser("b")
                .WithAlias("C")
                .Finish
                .AddParser("c")
                .WithAlias("C")
                .Finish
                .CreateParentChildRelationship("a", "b")
                .CreateParentChildRelationship("b", "c");

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("C C".Split(' '), builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Should().HaveCount(3);
        }
    }
}
