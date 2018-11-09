using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core.Help;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class HelpWriter_Should
    {
        [Fact]
        public void Return_An_Empty_String_For_An_Empty_Dom()
        {
            // arrange
            var sut = new HelpWriter();
            var root = new RootNode();

            // act
            var res = sut.CreateHelp(root);

            // assert
            res.Should().Be("");
        }
    }

    public class HelpWriterVisitor_Should
    {
        [Fact]
        public void Add_A_New_Line_For_Headings()
        {
            // arrange
            var visitor = new HelpWriterVisitor();

            // act
            visitor.Visit(new HeadingNode("hi"));

            // assert
            visitor.ToString().Should().Be(@"hi
");
        }

        [Fact]
        public void Add_A_New_Line_Before_Heading()
        {
            // arrange
            var visitor = new HelpWriterVisitor();

            // act
            visitor.Visit(new HeadingNode("hi"));

            // assert
            visitor.ToString().Should().Be(@"hi
");
        }
    }
}
