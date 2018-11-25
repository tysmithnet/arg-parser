using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class AliasRepository_Should
    {
        [Fact]
        public void Not_Throw_If_Alias_Doesnt_Exist_When_Looking_Up()
        {
            // arrange
            var repo = new AliasRepository();

            // act
            // assert
            repo.Lookup("whatever").Should().BeEmpty();
        }

        [Fact]
        public void Return_All_Commands_That_Match_Alias()
        {
            // arrange
            var repo = new AliasRepository();

            // act
            repo.SetAlias("1", "a");
            repo.SetAlias("2", "a");

            // assert
            repo.Lookup("a").Should().BeEquivalentTo("1 2".Split(' '));
        }

        [Fact]
        public void Return_The_Most_Recently_Set_Alias()
        {
            // arrange
            var repo = new AliasRepository();

            // act
            repo.SetAlias("a", "A");
            repo.SetAlias("a", "1");

            // assert
            repo.GetAlias("a").Should().Be("1");
        }

        [Fact]
        public void Return_True_Only_If_A_Parser_Has_An_Alias()
        {
            // arrange
            var repo = new AliasRepository();
            repo.SetAlias("a", "1");

            // act
            // assert
            repo.HasAlias("a").Should().BeTrue();
            repo.HasAlias("b").Should().BeFalse();
        }
    }
}