using System;
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
            var builder = new ContextBuilder("base");

            // act
            builder.AddParser("base");

            // assert
            builder.ParserRepository.Parsers.Should().HaveCount(1);
            builder.HierarchyRepository.Nodes.Should().HaveCount(1);
        }

        [Fact]
        public void Allow_Help_To_Be_Set_When_Adding()
        {
            // arrange
            var builder = new ContextBuilder("a");

            // act
            var parserBuilder = builder.AddParser("a",
                help => { help.SetName("Something").SetShortDescription("Does something"); });

            // assert
            parserBuilder.Parser.Help.Name.Should().Be("Something");
            parserBuilder.Parser.Help.ShortDescription.Should().Be("Does something");
        }

        [Fact]
        public void Correctly_Parse_A_Single_Help_Request()
        {
            // arrange
            var parseCount = 0;
            var builder = DefaultBuilder.CreateDefaultBuilder();

            // act
            var result = builder.Parse("-h".Split(' '));

            // assert
            result.When<UtilOptions>((options, parser) =>
            {
                options.IsHelpRequested.Should().BeTrue();
                parseCount++;
            });
            parseCount.Should().Be(1);
        }

        [Fact]
        public void Parse_Complicated_Hierarchies()
        {
            // arrange
            var args = "firewall block -p 8080 -m io firefox.exe".Split(' ');
            var builder = new ContextBuilder("base")
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
            var result = builder.Parse(args);

            // assert
            var isParsed = false;
            result.When<BlockProgramOptions>((options, parser) =>
            {
                isParsed = true;
                options.IsInbound.Should().BeTrue();
                options.IsOutbound.Should().BeTrue();
                options.Port.Should().Be(8080);
                options.Program.Should().Be("firefox.exe");
            });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Return_Any_Errors_That_Occurred_During_Processing()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();

            // act
            var res = builder.Parse("firewall -p -h".Split(' '));

            // assert
            var isParsed = false;
            res.WhenError(exceptions =>
            {
                exceptions.Should().HaveCount(1);
                isParsed = true;
            });
            isParsed.Should().BeTrue();
        }
    }
}