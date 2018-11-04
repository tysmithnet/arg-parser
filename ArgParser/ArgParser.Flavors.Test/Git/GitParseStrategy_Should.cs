using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArgParser.Core;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitParseStrategy_Should
    {
        public class BadFactory : DefaultIterationInfoFactory
        {
            public override IIterationInfo Create(string[] args) => new BadInfo
            {
                Args = args
            };
        }

        public class BadInfo : DefaultIterationInfo
        {
            public override IIterationInfo Consume(int numTokens) => base.Consume(-1);
        }

        [Fact]
        public void Only_Return_The_Most_Derived_Types()
        {
            // arrange
            var toFind = new FileNotFoundException();
            var results = new object[] {new Exception(), new IOException(), toFind};
            var strat = new GitParseStrategy(new GitContext());

            // act
            var result = strat.CreateParseResult(results.ToList());

            // assert
            var exceptionCount = 0;
            var ioCount = 0;
            var fileNotFoundCount = 0;
            result.When<Exception>(exception =>
            {
                exceptionCount++;
                exception.Should().BeSameAs(toFind);
            });
            result.When<IOException>(exception =>
            {
                ioCount++;
                exception.Should().BeSameAs(toFind);
            });
            result.When<FileNotFoundException>(exception =>
            {
                fileNotFoundCount++;
                exception.Should().BeSameAs(toFind);
            });
            exceptionCount.Should().Be(1);
            ioCount.Should().Be(1);
            fileNotFoundCount.Should().Be(1);
        }

        [Fact]
        public void Throw_If_Bad_Values_Provided()
        {
            // arrange
            var context = new GitContext();
            var strat = new GitParseStrategy(context);
            Action mightThrow0 = () => new GitParseStrategy(null);
            Action mightThrow1 = () => strat.Parse(null, null, null);
            Action mightThrow2 = () => strat.Parse(new IParser[0], null, null);
            Action mightThrow3 = () => strat.Parse(new IParser[0], new List<Func<object>>(), null);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Return_Only_A_NoFactoryFunctionError_If_No_Factory_Functions_Provided()
        {
            // arrange
            var context = new GitContext();
            var strat = new GitParseStrategy(context);

            // act
            var result = strat.Parse(new[] {new GitParser("base"),}, new Func<object>[0], "".Split(' '));

            // assert
            bool isParsed = false;
            result.OnError(errors =>
            {
                errors.Single().Message.Should().NotBeNullOrWhiteSpace();
                isParsed = true;
            });
            isParsed.Should().BeTrue();
        }

        // todo: this is bad, change IParser
        [Fact]
        public void Throw_Not_Implemented_Exception_For_Base_Parse()
        {
            // arrange
            var strat = new GitParseStrategy(new GitContext());
            Action mightThrow = () => strat.Parse(null, null);

            // act
            // assert
            mightThrow.Should().Throw<NotImplementedException>();
        }
    }
}