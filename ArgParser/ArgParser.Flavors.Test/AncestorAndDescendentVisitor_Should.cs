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
            var a = new GitFlavor("a");
            var b = new GitFlavor("b");
            var aa = new GitFlavor("aa");
            var ab = new GitFlavor("ab");
            var aaa = new GitFlavor("aaa");
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