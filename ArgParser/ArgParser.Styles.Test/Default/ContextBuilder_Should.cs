using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ContextBuilder_Should
    {
        [Fact]
        public void Add_New_Parsers_To_All_Necessary_Repositories()
        {
            // arrange
            var builder = new ContextBuilder();
            
            // act
            builder.AddParser("base");

            // assert
            builder.ParserRepo.Parsers.Should().HaveCount(1);
            builder.HierarchyRepository.Nodes.Should().HaveCount(1);
        }
    }
}