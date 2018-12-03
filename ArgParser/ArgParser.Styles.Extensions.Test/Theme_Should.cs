using System;
using Figgle;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class Theme_Should
    {
        [Fact]
        public void Allow_New_Themes_To_Be_Created()
        {
            // arrange
            var theme = Theme.Create(FiggleFonts.Reverse, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue,
                ConsoleColor.Magenta);

            // act
            // assert
            theme.DefaultTextColor.Should().Be(ConsoleColor.Red);
            theme.SecondaryTextColor.Should().Be(ConsoleColor.Yellow);
            theme.CodeColor.Should().Be(ConsoleColor.Green);
            theme.RequiredColor.Should().Be(ConsoleColor.Blue);
            theme.BorderColor.Should().Be(ConsoleColor.Magenta);
        }

        [Fact]
        public void Allow_Values_To_Be_Updated()
        {
            // arrange
            var theme = Theme.Create(FiggleFonts.Reverse, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue,
                ConsoleColor.Magenta);
            theme.DefaultTextColor = ConsoleColor.White;
            theme.SecondaryTextColor = ConsoleColor.White;
            theme.CodeColor = ConsoleColor.White;
            theme.RequiredColor = ConsoleColor.White;
            theme.BorderColor = ConsoleColor.White;

            // act
            // assert
            theme.DefaultTextColor.Should().Be(ConsoleColor.White);
            theme.SecondaryTextColor.Should().Be(ConsoleColor.White);
            theme.CodeColor.Should().Be(ConsoleColor.White);
            theme.RequiredColor.Should().Be(ConsoleColor.White);
            theme.BorderColor.Should().Be(ConsoleColor.White);
        }
    }
}