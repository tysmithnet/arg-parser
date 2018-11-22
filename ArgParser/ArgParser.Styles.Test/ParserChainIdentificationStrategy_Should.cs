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
    public class ParserChainIdentificationStrategy_Should
    {
        [Fact]
        public void Allow_Aliases_To_Be_Used()
        {
            // arrange
            var builder = new ContextBuilder()
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
            res.Chain.Should().BeEquivalentTo("a b c".Split(' ').Select(x => builder.Context.ParserRepository.Get(x)));
        }

        [Fact]
        public void Find_The_Correct_Alias_If_Multiple_Exist()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("a")
                .Finish
                .AddParser("b")
                .WithAlias("C")
                .Finish
                .AddParser("c")
                .WithAlias("C")
                .Finish
                .AddParser("d")
                .Finish
                .CreateParentChildRelationship("a", "b")
                .CreateParentChildRelationship("b", "c")
                .CreateParentChildRelationship("c", "d");

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("C C d".Split(' '), builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Should().BeEquivalentTo("a b c d".Split(' ').Select(x => builder.Context.ParserRepository.Get(x)));
            res.ConsumedArgs.Should().BeEquivalentTo("C C d".Split(' '));
        }

        [Fact]
        public void Allow_Either_The_Alias_Or_The_Id_To_Be_Used()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("a")
                .Finish
                .AddParser("b")
                .WithAlias("C")
                .Finish
                .AddParser("c")
                .WithAlias("C")
                .Finish
                .AddParser("d")
                .Finish
                .CreateParentChildRelationship("a", "b")
                .CreateParentChildRelationship("b", "c")
                .CreateParentChildRelationship("c", "d");

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("b C d".Split(' '), builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Should().BeEquivalentTo("a b c d".Split(' ').Select(x => builder.Context.ParserRepository.Get(x)));
            res.ConsumedArgs.Should().BeEquivalentTo("b C d".Split(' '));
        }

        [Fact]
        public void Throw_If_There_Is_An_Ambiguous_Match()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("a")
                .Finish
                .AddParser("b")
                .WithAlias("C")
                .Finish
                .AddParser("c")
                .WithAlias("C")
                .Finish
                .CreateParentChildRelationship("a", "b")
                .CreateParentChildRelationship("a", "c");

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("C C".Split(' '), builder.Context);
            Action mightThrow = () => strat.Identify(request);
            
            // act
            
            // assert
            mightThrow.Should().Throw<AmbiguousCommandChainException>();
        }

        [Fact]
        public void Return_The_Root_If_There_Are_No_Commands()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("a")
                .Finish;

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("C C".Split(' '), builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Single().Should().Be(builder.Context.RootParser());
            res.ConsumedArgs.Should().BeEmpty();
        }

        [Fact]
        public void Return_The_Root_If_There_Are_No_Args()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("a")
                .Finish;

            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest(new string[0], builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Single().Should().Be(builder.Context.RootParser());
            res.ConsumedArgs.Should().BeEmpty();
        }

        [Fact]
        public void Work_When_No_Aliases_Are_Defined()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("a")
                .Finish
                .AddParser("b")
                .Finish
                .AddParser("c")
                .Finish
                .AddParser("d")
                .Finish
                .AddParser("e")
                .Finish
                .CreateParentChildRelationship("a", "b")
                .CreateParentChildRelationship("b", "c")
                .CreateParentChildRelationship("c", "d")
                .CreateParentChildRelationship("a", "e");
            var strat = new ParserChainIdentificationStrategy(builder.Context);
            var request = new ChainIdentificationRequest("b c d".Split(' '), builder.Context);

            // act
            var res = strat.Identify(request);

            // assert
            res.Chain.Should()
                .BeEquivalentTo("a b c d".Split(' ').Select(x => builder.Context.ParserRepository.Get(x)));
        }
    }
}
