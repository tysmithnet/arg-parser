using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class ThemeRepository_Should
    {
        [Fact]
        public void Allow_A_Theme_To_Be_Added_For_A_Parser()
        {
            // arrange
            var repo = new ThemeRepository()
            {
                Themes = new Dictionary<string, Theme>()
                {
                    ["a"] = Theme.Cool
                }
            };

            // act
            repo.SetTheme("a", Theme.Warm);

            // assert
            repo.Get("a").Should().BeSameAs(Theme.Warm);
        }
    }
}