using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class AncestorAndDescendentVisitor_Should
    {
        [Fact]
        public void Find_All_Correct_Values()
        {
            // arrange
            var a = new GitParser("a");
            var b = new GitParser("b");
            var aa = new GitParser("aa");
            var ab = new GitParser("ab");
            var aaa = new GitParser("aaa");
            a.AddSubCommand("b", b);
            a.AddSubCommand("aa", aa);
            a.AddSubCommand("ab", ab);
            aa.AddSubCommand("aaa", aaa);
            var visitor = new AncestorAndDescendentVisitor();

            // act
            aa.Accept(visitor);

            // assert
            visitor.GitFlavors.Should().BeEquivalentTo(a, aa, aaa);
        }
    }
}