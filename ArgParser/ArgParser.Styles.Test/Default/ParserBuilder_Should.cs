using System;
using System.Linq;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ParserBuilder_Should
    {
        [Fact]
        public void Allow_A_Positional_Of_A_Single_String()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser0 = new Parser<string>("base");
            var builder0 = new ParserBuilder<string>(contextBuilder, parser0);
            var parser1 = new Parser("base");
            var builder1 = new ParserBuilder(contextBuilder, parser1);

            // act
            builder0.WithPositional((o, s) => { });
            builder1.WithPositional((o, s) => { });

            // assert
            builder0.Parser.Parameters.Single().Should().BeAssignableTo<Positional<string>>();
            builder1.Parser.Parameters.Single().Should().BeAssignableTo<Positional>();
        }

        [Fact]
        public void Allow_A_Positional_Of_Multiple_Values()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser0 = new Parser<string>("base");
            var builder0 = new ParserBuilder<string>(contextBuilder, parser0);
            var parser1 = new Parser("base");
            var builder1 = new ParserBuilder(contextBuilder, parser1);

            // act
            builder0.WithPositionals((o, s) => { }, 1, 1);
            builder1.WithPositionals((o, s) => { }, 1, 1);

            // assert
            builder0.Parser.Parameters.Single().Should().BeAssignableTo<Positional<string>>();
            builder1.Parser.Parameters.Single().Should().BeAssignableTo<Positional>();
        }

        [Fact]
        public void Allow_Boolean_Switches()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser0 = new Parser<string>("base");
            var builder0 = new ParserBuilder<string>(contextBuilder, parser0);
            var parser1 = new Parser("base");
            var builder1 = new ParserBuilder(contextBuilder, parser1);

            // act
            builder0.WithBooleanSwitch('h', "help", s => { });
            builder1.WithBooleanSwitch('h', "help", s => { });

            // assert
            builder0.Parser.Parameters.Single().Should().BeAssignableTo<BooleanSwitch<string>>();
            builder1.Parser.Parameters.Single().Should().BeAssignableTo<BooleanSwitch>();
        }

        [Fact]
        public void Allow_Boolean_Switches_To_Be_Added()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser = new Parser("base");
            var builder = new ParserBuilder(contextBuilder, parser);
            var parseCount = 0;

            // act
            builder.WithBooleanSwitch('h', "help", o => parseCount++);

            // assert
            var result = (BooleanSwitch) parser.Parameters.Single();
            result.Letter.Should().Be('h');
            result.Word.Should().Be("help");
        }

        [Fact]
        public void Allow_Multi_Value_Switches()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser0 = new Parser<string>("base");
            var builder0 = new ParserBuilder<string>(contextBuilder, parser0);
            var parser1 = new Parser("base");
            var builder1 = new ParserBuilder(contextBuilder, parser1);

            // act
            builder0.WithValuesSwitch('h', "help", (o, s) => { });
            builder1.WithValuesSwitch('h', "help", (o, s) => { });

            // assert
            builder0.Parser.Parameters.Single().Should().BeAssignableTo<ValuesSwitch<string>>();
            builder1.Parser.Parameters.Single().Should().BeAssignableTo<ValuesSwitch>();
        }

        [Fact]
        public void Allow_Single_Value_Switches()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser0 = new Parser<string>("base");
            var builder0 = new ParserBuilder<string>(contextBuilder, parser0);
            var parser1 = new Parser("base");
            var builder1 = new ParserBuilder(contextBuilder, parser1);

            // act
            builder0.WithSingleValueSwitch('h', "help", (o, s) => { });
            builder1.WithSingleValueSwitch('h', "help", (o, s) => { });

            // assert
            builder0.Parser.Parameters.Single().Should().BeAssignableTo<SingleValueSwitch<string>>();
            builder1.Parser.Parameters.Single().Should().BeAssignableTo<SingleValueSwitch>();
        }

        [Fact]
        public void Provide_A_Generic_Version()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser = new Parser<string>("base");
            var builder = new ParserBuilder<string>(contextBuilder, parser);

            // act
            // assert
            builder.Finish.Should().Be(contextBuilder);
        }

        [Fact]
        public void Return_The_Same_ContextBuilder_It_Was_Created_With()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser = new Parser("base");
            var builder = new ParserBuilder(contextBuilder, parser);

            // act
            // assert
            builder.Finish.Should().BeSameAs(contextBuilder);
        }

        [Fact]
        public void Throw_If_Multiple_Strings_Passed_To_The_Single_String_Callback()
        {
            // arrange
            var contextBuilder = new ContextBuilder();
            var parser0 = new Parser<string>("base");
            var builder0 = new ParserBuilder<string>(contextBuilder, parser0);
            var parser1 = new Parser("base");
            var builder1 = new ParserBuilder(contextBuilder, parser1);

            // act
            builder0.WithPositional((o, s) => { });
            builder1.WithPositional((o, s) => { });

            // assert
            Action mightThrow0 = () => builder0.Parser.Parameters.Single().ConsumeCallback("", "a b".Split(' '));
            mightThrow0.Should().Throw<InvalidOperationException>();
            Action mightThrow1 = () =>
                builder1.Parser.Parameters.Single().ConsumeCallback(new object(), "a b".Split(' '));
            mightThrow1.Should().Throw<InvalidOperationException>();
        }
    }
}