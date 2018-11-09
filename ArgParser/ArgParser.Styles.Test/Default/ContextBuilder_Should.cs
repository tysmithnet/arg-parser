using System;
using ArgParser.Styles.Default;
using ArgParser.Testing.Common;
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
            builder.ParserRepository.Parsers.Should().HaveCount(1);
            builder.HierarchyRepository.Nodes.Should().HaveCount(1);
        }

        [Fact]
        public void Parse_Complicated_Hierarchies()
        {
            // arrange
            var args = "firewall block -p 8080 -m io firefox.exe".Split(' ');
            var builder = new ContextBuilder()
                .AddParser<UtilOptions>("base")
                .WithBooleanSwitch('h', "help", o => o.IsHelpRequested = true)
                .WithBooleanSwitch(null, "version", o => o.IsVersionRequested = true)
                .Finish
                .AddParser<ClipboardOptions>("clip")
                .WithBooleanSwitch('o', "overwrite", o => o.IsOverwriteClipboard = true)
                .Finish
                .AddParser<SortOptions>("sort")
                .WithFactoryFunction(() => new SortOptions())
                .WithBooleanSwitch('r', "reverse", o => o.IsReversed = true)
                .Finish
                .AddParser<ZipOptions>("zip")
                .WithFactoryFunction(() => new ZipOptions())
                .WithPositional((o, s) => o.ZipFile = s)
                .WithPositionals((o, s) => o.Globs = s)
                .Finish
                .AddParser<FireWallOptions>("firewall")
                .WithSingleValueSwitch('p', "port", (o, s) => o.Port = Convert.ToInt32(s))
                .WithSingleValueSwitch('m', "mode", (o, s) =>
                {
                    o.IsInbound = s.Contains("i");
                    o.IsOutbound = s.Contains("o");
                })
                .WithPositional((o, s) => o.Program = s)
                .Finish
                .AddParser<BlockProgramOptions>("block")
                .WithFactoryFunction(() => new BlockProgramOptions())
                .Finish
                .AddParser<UnblockProgramOptions>("unblock")
                .WithFactoryFunction(() => new UnblockProgramOptions())
                .Finish
                .AddParser<ConvertOptions>("convert")
                .WithFactoryFunction(() => new ConvertOptions())
                .WithSingleValueSwitch('f', "format", (o, s) => o.Format = s)
                .WithPositionals((o, s) => o.InputFiles = s)
                .Finish
                .CreateParentChildRelationship("base", "clip")
                .CreateParentChildRelationship("base", "firewall")
                .CreateParentChildRelationship("base", "convert")
                .CreateParentChildRelationship("clip", "sort")
                .CreateParentChildRelationship("clip", "zip")
                .CreateParentChildRelationship("firewall", "block")
                .CreateParentChildRelationship("firewall", "unblock");

            // act
            var result = builder.Parse("base", args);

            // assert
            var isParsed = false;
            result.When<BlockProgramOptions>(options =>
            {
                isParsed = true;
                options.IsInbound.Should().BeTrue();
                options.IsOutbound.Should().BeTrue();
                options.Port.Should().Be(8080);
                options.Program.Should().Be("firefox.exe");
            });
            isParsed.Should().BeTrue();
        }
    }
}