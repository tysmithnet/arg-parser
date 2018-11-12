using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Core.Test
{
    public class ContextExtensions_Should
    {
        [Fact]
        public void Return_The_Path_To_Root_For_A_Parser()
        {
            // arrange
            var contextMock = new Mock<IContext>();
            var hierarchyRepoMock = new Mock<IHierarchyRepository>();
            hierarchyRepoMock.Setup(repo => repo.GetAncestors(It.IsAny<string>()))
                .Returns(new[] {"parent", "grandparent"});
            var parserRepoMock = new Mock<IParserRepository>();
            parserRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).Returns((string s) => new Parser(s));

            contextMock.Setup(context => context.HierarchyRepository).Returns(hierarchyRepoMock.Object);
            contextMock.Setup(context => context.ParserRepository).Returns(parserRepoMock.Object);

            // act
            // assert
            contextMock.Object.PathToRoot("child").Select(p => p.Id).Should().BeEquivalentTo(new[] {"child", "parent", "grandparent"});
        }
    }
}
