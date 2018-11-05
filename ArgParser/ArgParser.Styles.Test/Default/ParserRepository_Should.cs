using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ParserRepository_Should
    {
        [Fact]
        public void Throw_If_Cant_Find_Requested_Parser()
        {
            // arrange
            var repo = new ParserRepository();
            Action mightThrow0 = () => repo.Get("test");

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Adding_Same_Id()
        {
            // arrange
            var repo = new ParserRepository();
            repo.Create("a");
            Action mightThrow0 = () => repo.Create("a");

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentException>();
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
    }
}
