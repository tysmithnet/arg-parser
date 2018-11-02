using System;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class ParserBuilder_Should
    {
        [Fact]
        public void Add_Factory_Functions()
        {
            // arrange
            var parent = new GitBuilder();
            var context = new GitContext();
            var builder = new ParserBuilder("base", parent, context);

            // act
            builder.WithFactoryFunctions(() => new object(), () => "");

            // assert
            context.FactoryFunctionRepository.GetFactoryFunctions("base").Should().HaveCount(2);
        }

        [Fact]
        public void Add_Positionals()
        {
            // arrange
            var parent = new GitBuilder();
            var context = new GitContext();
            var builder = new ParserBuilder("base", parent, context);

            // act
            builder.WithPositional((o, strings) => { });

            // assert
            context.ParameterRepository.GetPositionals("base").Should().HaveCount(1);
        }

        [Fact]
        public void Add_Switches()
        {
            // arrange
            var parent = new GitBuilder();
            var context = new GitContext();
            var builder = new ParserBuilder("base", parent, context);

            // act
            builder.WithBooleanSwitch('h', "help", o => { });
            builder.WithSingleValueSwitch('v', "value", (o, s) => { });
            builder.WithValueSwitch('x', "extra", (o, strings) => { });

            // assert
            context.ParameterRepository.GetBooleanSwitches("base").Should().HaveCount(1);
            context.ParameterRepository.GetSwitches("base").Should().HaveCount(3);
        }

        [Fact]
        public void Add_Validation()
        {
            // arrange
            var parent = new GitBuilder();
            var context = new GitContext();
            var builder = new ParserBuilder("base", parent, context);

            // act
            builder.WithValidation(new RequiredParameterValidator(new BooleanSwitch()));

            // assert
            context.ValidatorRepository.GetValidators("base").Should().HaveCount(1);
        }

        [Fact]
        public void Return_The_Parent_When_Building()
        {
            // arrange
            var parent = new GitBuilder();
            var context = new GitContext();
            var builder = new ParserBuilder("base", parent, context);

            // act
            builder.WithValidation(new RequiredParameterValidator(new BooleanSwitch()));

            // assert
            builder.Build().Should().BeSameAs(parent);
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var parent = new GitBuilder();
            var context = new GitContext();
            Action mightThrow0 = () => new ParserBuilder(null, null, null);
            Action mightThrow1 = () => new ParserBuilder("a", null, null);
            Action mightThrow2 = () => new ParserBuilder("a", parent, null);
            Action mightThrow3 = () => new ParserBuilder("a", parent, context);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().NotThrow();
        }
    }
}