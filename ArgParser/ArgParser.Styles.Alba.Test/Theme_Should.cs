using System;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class Theme_Should
    {
        [Fact]
        public void Allow_New_Themes_To_Be_Created()
        {
            // arrange
            var theme = Theme.Create(ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue,
                ConsoleColor.Magenta);

            // act
            // assert
            theme.DefaultTextColor.Should().Be(ConsoleColor.Red);
            theme.SecondaryTextColor.Should().Be(ConsoleColor.Yellow);
            theme.CodeColor.Should().Be(ConsoleColor.Green);
            theme.RequiredColor.Should().Be(ConsoleColor.Blue);
            theme.BorderColor.Should().Be(ConsoleColor.Magenta);
        }
    }
}