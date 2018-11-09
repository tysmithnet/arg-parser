using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
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