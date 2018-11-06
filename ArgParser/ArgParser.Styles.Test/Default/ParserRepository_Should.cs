using System;
using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ParserRepository_Should
    {
        [Fact]
        public void Get_A_Registered_Generic_Parser()
        {
            // arrange
            var repo = new ParserRepository();

            // act
            repo.Create<string>("string");

            // assert
            repo.Get<string>("string").Should().BeOfType<Parser<string>>();
        }

        [Fact]
        public void Return_All_Previously_Created_Parsers()
        {
            // arrange
            var repo = new ParserRepository();
            repo.Create("a");
            repo.Create("b");

            // act
            // assert
            repo.GetAll().Should().HaveCount(2);
        }

        [Fact]
        public void Return_Previously_Created_Parsers()
        {
            // arrange
            var repo = new ParserRepository();
            repo.Create("a");
            repo.Create("b");
            Action mightThrow = () => repo.Get("c");

            // act
            // assert
            repo.Get("a").Should().BeOfType<Parser>();
            repo.Get("b").Should().BeOfType<Parser>();
            mightThrow.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Adding_Same_Id()
        {
            // arrange
            var repo = new ParserRepository();
            repo.Create("a");
            Action mightThrow0 = () => repo.Create("a");
            Action mightThrow1 = () => repo.Create<string>("a");

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentException>();
            mightThrow1.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Throw_If_Cant_Find_Requested_Parser()
        {
            // arrange
            var repo = new ParserRepository();
            Action mightThrow0 = () => repo.Get("test");
            Action mightThrow1 = () => repo.Get<string>("test");

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
            mightThrow1.Should().Throw<KeyNotFoundException>();
        }
    }
}