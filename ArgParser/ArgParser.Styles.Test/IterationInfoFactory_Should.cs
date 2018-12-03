using System.Linq;
using ArgParser.Core;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class IterationInfoFactory_Should
    {
        [Fact]
        public void Create_The_Correct_Info_For_The_Request()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new IterationInfoFactory(builder.Context);
            var parsers = builder.Context.ParserRepository.Get("util").ToEnumerableOfOne();
            var res = new ChainIdentificationResult(parsers, new string[0]);
            var req = new IterationInfoRequest(res, "-h".Split(' '), "-hlep".Split(' '));

            // act
            var result = fac.Create(req);

            // assert
            result.Args.Should().BeEquivalentTo("-h".Split(' '));
            result.Index.Should().Be(0);
        }

        [Fact]
        public void Make_The_Index_Reflect_The_Intersection_Of_The_Original_And_Mutated_Args()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new IterationInfoFactory(builder.Context);
            var parsers = builder.Context.ParserRepository.Get("util").ToEnumerableOfOne();
            var res = new ChainIdentificationResult(parsers, new string[0]);
            var req = new IterationInfoRequest(res, "-h".Split(' '), "-h -h -h".Split(' '));

            // act
            var result = fac.Create(req);

            // assert
            result.Args.Should().BeEquivalentTo("-h".Split(' '));
            result.Index.Should().Be(0);
        }

        [Fact]
        public void Recognize_Parser_Chains()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new IterationInfoFactory(builder.Context);
            var parsers = builder.Context.PathToRoot("firewall").Reverse();
            var res = new ChainIdentificationResult(parsers, new []{"firewall"});
            var req = new IterationInfoRequest(res, "firewall -h".Split(' '), "firewall -h -h -h".Split(' '));

            // act
            var result = fac.Create(req);

            // assert
            result.Args.Should().BeEquivalentTo("firewall -h".Split(' '));
            result.Index.Should().Be(1);
        }

        [Fact]
        public void Only_Consume_Until_The_Mutated_Args_No_Longer_Match()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new IterationInfoFactory(builder.Context);
            var parsers = builder.Context.PathToRoot("firewall").Reverse();
            var res = new ChainIdentificationResult(parsers, new[] { "firewall" });
            var req = new IterationInfoRequest(res, "firewall -s -l".Split(' '), "firewall -h -h -h".Split(' '));

            // act
            var result = fac.Create(req);

            // assert
            result.Args.Should().BeEquivalentTo("firewall -s -l".Split(' '));
            result.Index.Should().Be(1);
        }
    }
}