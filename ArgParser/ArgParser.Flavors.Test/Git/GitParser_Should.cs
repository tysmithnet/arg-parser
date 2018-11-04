using System;
using System.Linq;
using ArgParser.Core;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitParser_Should
    {
        [Fact]
        public void Indicate_It_Can_Consume_If_It_Or_Its_Base_Parser_Can_Consume()
        {
            // arrange
            var context = new GitContext();
            var baseParser = context.ParserRepository.Create("base");
            baseParser.Context = context;
            context.ParameterRepository.AddParameter("base", new BooleanSwitch('h', "help", o => { }));
            var childParser = context.ParserRepository.Create("child");
            childParser.Context = context;
            context.ParameterRepository.AddParameter("child", new BooleanSwitch('v', "verbose", o => { }));
            context.ParserRepository.EstablishParentChildRelationship("base", "child");
            baseParser.Reset();
            childParser.Reset();
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-h".Split(' '));
            var info1 = fac.Create("-v".Split(' '));

            // act
            // assert
            childParser.CanConsume(new object(), info0).Should().BeTrue();
            childParser.CanConsume(new object(), info1).Should().BeTrue();
        }

        [Fact]
        public void Parse_Args_Correctly()
        {
            // arrange
            var context = new GitContext();
            var baseParser = context.ParserRepository.Create("base");
            baseParser.Context = context;
            var isHelp = false;
            var isVerbose = true;
            context.ParameterRepository.AddParameter("base", new BooleanSwitch('h', "help", o => { isHelp = true; }));
            var childParser = context.ParserRepository.Create("child");
            childParser.Context = context;
            context.ParameterRepository.AddParameter("child",
                new BooleanSwitch('v', "verbose", o => { isVerbose = true; }));
            context.ParserRepository.EstablishParentChildRelationship("base", "child");
            baseParser.Reset();
            childParser.Reset();
            context.FactoryFunctionRepository.AddFactoryFunction("base", () => new object());
            context.FactoryFunctionRepository.AddFactoryFunction("child", () => new object());

            // act
            // assert
            childParser.Parse("-h -v".Split(' '));
            isHelp.Should().BeTrue();
            isVerbose.Should().BeTrue();
        }

        [Fact]
        public void Parse_Using_SubCommands()
        {
            // arrange
            var context = new GitContext();
            var baseParser = context.ParserRepository.Create("base");
            baseParser.Context = context;
            var isHelp = false;
            var isVerbose = true;
            context.ParameterRepository.AddParameter("base", new BooleanSwitch('h', "help", o => { isHelp = true; }));
            var childParser = context.ParserRepository.Create("child");
            childParser.Context = context;
            context.ParameterRepository.AddParameter("child",
                new BooleanSwitch('v', "verbose", o => { isVerbose = true; }));
            context.ParserRepository.EstablishParentChildRelationship("base", "child");
            baseParser.Reset();
            childParser.Reset();
            context.FactoryFunctionRepository.AddFactoryFunction("base", () => new object());
            context.FactoryFunctionRepository.AddFactoryFunction("child", () => new object());

            // act
            // assert
            baseParser.Parse("child -h -v".Split(' '));
            isHelp.Should().BeTrue();
            isVerbose.Should().BeTrue();
        }

        [Fact]
        public void Return_The_Correct_BaseParser()
        {
            // arrange
            var context = new GitContext();
            var baseParser = context.ParserRepository.Create("base");
            baseParser.Context = context;
            var isHelp = false;
            var isVerbose = true;
            context.ParameterRepository.AddParameter("base", new BooleanSwitch('h', "help", o => { isHelp = true; }));
            var childParser = context.ParserRepository.Create("child");
            childParser.Context = context;
            context.ParameterRepository.AddParameter("child",
                new BooleanSwitch('v', "verbose", o => { isVerbose = true; }));
            context.ParserRepository.EstablishParentChildRelationship("base", "child");
            baseParser.Reset();
            childParser.Reset();
            context.FactoryFunctionRepository.AddFactoryFunction("base", () => new object());
            context.FactoryFunctionRepository.AddFactoryFunction("child", () => new object());

            // act
            // assert
            childParser.BaseParser.Should().BeSameAs(baseParser);
        }

        [Fact]
        public void Should_Return_Help()
        {
            // arrange
            var context = new GitContext();
            var parser = context.ParserRepository.Create("base");

            // act
            // assert
            parser.Help.Should().BeNull();
        }

        [Fact]
        public void Throw_If_There_Is_No_Way_To_Consume()
        {
            // arrange
            var context = new GitContext();
            var parser = context.ParserRepository.Create("base");
            parser.Context = context;
            parser.Reset();

            // *no parameters added*
            Action mightThrow = () => parser.Consume(new object(), new DefaultIterationInfo());

            // act
            // assert
            mightThrow.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Allow_For_Required_Parameters()
        {
            // arrange
            var builder = new GitBuilder()
                .AddParser("base")
                .WithBooleanSwitch('v', "verbose", o => { }, true)
                .WithFactoryFunctions(() => new object())
                .Build();

            // act
            var result = builder.Parse("base", "".Split(' '));

            // assert
            bool isParsed = false;
            result.OnError(errors =>
            {
                isParsed = true;
                errors.Single().Should().BeOfType<MissingRequiredParameterError>();
            });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Use_Base_Parsers_To_Consume_If_Necessary()
        {
            // arrange
            var context = new GitContext();
            var baseParser = context.ParserRepository.Create("base");
            baseParser.Context = context;
            var isHelp = false;
            var isVerbose = true;
            context.ParameterRepository.AddParameter("base", new BooleanSwitch('h', "help", o => { isHelp = true; }));
            var childParser = context.ParserRepository.Create("child");
            childParser.Context = context;
            context.ParameterRepository.AddParameter("child",
                new BooleanSwitch('v', "verbose", o => { isVerbose = true; }));
            context.ParserRepository.EstablishParentChildRelationship("base", "child");
            baseParser.Reset();
            childParser.Reset();
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-h -v".Split(' '));

            // act
            // assert
            childParser.Consume(new object(), info0);
            isHelp.Should().BeTrue();
            isVerbose.Should().BeTrue();
        }
    }
}