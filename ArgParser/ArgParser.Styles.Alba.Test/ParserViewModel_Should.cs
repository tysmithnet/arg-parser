using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ParserViewModel_Should
    {
        [Fact]
        public void Return_The_Alias_If_Set_Otherwise_The_Id()
        {
            // arrange
            var vm0 = new ParserViewModel(new Parser("a"), Theme.Basic)
            {
                Alias = "A"
            };

            var vm1 = new ParserViewModel(new Parser("a"), Theme.Basic);

            // act
            // assert
            vm0.DisplayString.Should().Be("A");
            vm1.DisplayString.Should().Be("a");
        }
    }
}