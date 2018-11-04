using System;
using System.Text;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitBuilder_Should
    {
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
        public void Allow_For_Required_Parameters()
        {
            // arrange
            var builder = new GitBuilder();
            builder
                .AddParser("base")
                .WithBooleanSwitch('v', "verbose", o => { }, isRequired:true);

            // act
            // assert
            bool isParsed = false;
            builder.Parse("base", "".Split(' '))
                .OnError(errors => {
                    errors.Should().HaveCount(1);
                    isParsed = true;
                });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Offer_Generic_Counterparts()
        {
            // arrange
            var builder = new GitBuilder();

            // act
            var result = builder.AddParser<StringBuilder>("base");

            // assert
            result.Should().BeOfType<ParserBuilder<StringBuilder>>();
        }

        [Fact]
        public void Parse_Using_The_Chosen_Parser()
        {
            // arrange
            var builder = new GitBuilder();
            var isBaseCalled = false;
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
    }
}