using System;
using System.Collections.Generic;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitFactoryFunctionRepository_Should
    {
        [Fact]
        public void Return_All_Registered_Factory_Functions()
        {
            // arrange
            var repo = new GitFactoryFunctionRepository();

            // act
            repo.AddFactoryFunction("a", () => "");
            repo.AddFactoryFunction("a", () => 42);

            // assert
            repo.GetFactoryFunctions("a").Should().HaveCount(2);
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var repo = new GitFactoryFunctionRepository();
            Action mightThrow0 = () => repo.AddFactoryFunction(null, null);
            Action mightThrow1 = () => repo.AddFactoryFunction("a", null);
            Action mightThrow2 = () => repo.AddFactoryFunction(null, () => "");

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_No_Matching_Parser_Name()
        {
            // arrange
            var repo = new GitFactoryFunctionRepository();
            Action mightThrow = () => repo.GetFactoryFunctions("doesntexist");

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }
    }
}