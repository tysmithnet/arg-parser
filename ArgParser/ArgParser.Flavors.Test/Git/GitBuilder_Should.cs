using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitBuilder_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var builder = new GitBuilder();
            Action mightThrow0 = () => builder.AddParser(null);
            Action mightThrow1 = () => builder.AddSubCommand(null, null);
            Action mightThrow2 = () => builder.AddSubCommand("a", null);
            Action mightThrow3 = () => builder.AddSubCommand(null, "b");
            Action mightThrow4 = () => builder.Parse(null, null);
            Action mightThrow5 = () => builder.Parse("a", null);
            Action mightThrow6 = () => builder.Parse(null, "a b".Split(' '));

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().Throw<ArgumentNullException>();
            mightThrow4.Should().Throw<ArgumentNullException>();
            mightThrow5.Should().Throw<ArgumentNullException>();
            mightThrow6.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Establish_SubCommand_Relationships()
        {
            // arrange
            var builder = new GitBuilder();
            builder
                .AddParser("base")
                .Build()
                .AddParser("child")
                .Build()
                .AddSubCommand("base", "child");

            // act
            // assert
            builder.Context.ParserRepository.IsSubCommand("base", "child").Should().BeTrue();
        }

        [Fact]
        public void Parse_Using_The_Chosen_Parser()
        {
            // arrange
            var builder = new GitBuilder();
            bool isBaseCalled = false;
            builder
                .AddParser("base")
                .WithFactoryFunctions(() => new object())
                .WithPositional((o, strings) => isBaseCalled = true)
                .Build()
                .AddParser("child")
                .Build()
                .AddSubCommand("base", "child");

            // act
            // assert
            builder.Parse("base", "a b".Split(' '));
            isBaseCalled.Should().BeTrue();
        }
    }
}
