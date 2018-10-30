using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var a = new GitFlavor();
            var b = new GitFlavor();
            var aa = new GitFlavor();
            var ab = new GitFlavor();
            var aaa = new GitFlavor();
            a.AddSubCommand("b", b);
            a.AddSubCommand("aa", aa);
            a.AddSubCommand("ab", ab);
            aa.AddSubCommand("aaa", aaa);
            var visitor = new AncestorAndDescendentVisitor();

            // act
            aa.Accept(visitor);

            // assert
            visitor.GitFlavors.Should().BeEquivalentTo(new[] {a, aa, aaa});
        }
    }
}
