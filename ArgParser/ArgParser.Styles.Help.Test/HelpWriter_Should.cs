using ArgParser.Styles.Help;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
    public class HelpWriter_Should
    {
        [Fact]
        public void Return_An_Empty_String_For_An_Empty_Dom()
        {
            // arrange
            var sut = new Styles.Help.HelpWriter();
            var root = new RootNode();

            // act
            var res = sut.CreateHelp(root);

            // assert
            res.Should().Be("");
        }
    }
}
