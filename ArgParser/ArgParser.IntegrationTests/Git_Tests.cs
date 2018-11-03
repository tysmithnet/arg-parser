using System;
using ArgParser.Flavors.Git;
using ArgParser.IntegrationTests.Options.MadeUpUtility;
using ArgParser.IntegrationTests.Options.Trivial;
using FluentAssertions;
using Xunit;

namespace ArgParser.IntegrationTests
{
    public class Git_Tests
    {
        [Fact]
        public void Basic_Hierarchy_Parsing()
        {
            // arrange
            var builder = new GitBuilder();

            // act
            builder.AddParser<BaseOptions>("base")
                .WithBooleanSwitch('h', "help", options => options.IsHelpRequested = true)
                .WithBooleanSwitch('v', "version", options => options.IsVersionRequested = true)
                .Build()
                .AddParser<TestOptions>("test")
                .WithSingleValueSwitch('c', "config", (options, s) => options.ConfigFile = s)
                .WithPositionals((options, strings) => options.TestConfigurations.AddRange(strings))
                .Build()
                .AddParser<ExtraTestOptions>("extra")
                .WithBooleanSwitch('s', "secret", options => options.TestSecretFiles = true)
                .WithFactoryFunctions(() => new ExtraTestOptions())
                .Build()
                .AddSubCommand("test", "extra")
                .AddSubCommand("base", "test");

            // assert
            var result = builder.Parse("base", "test extra t0 t1 t2 -s -c betterconfig".Split(' '));
            var parseCount = 0;
            result.When<ExtraTestOptions>(options =>
            {
                parseCount++;
                options.ConfigFile.Should().Be("betterconfig");
                options.TestConfigurations.Should().BeEquivalentTo("t0 t1 t2".Split(' '));
                options.TestSecretFiles.Should().BeTrue();
            });
            parseCount.Should().Be(1);
        }

        [Fact]
        public void Intermediate_Hiearchy_Parsing()
        {
            // arrange
            var builder = CreateUtilBuilder();

            // act
            var result = builder.Parse("base", "firewall block -p 8080 -m io firefox.exe".Split(' '));

            // assert
            var isParsed = false;
            result.When<BlockProgramOptions>(options =>
            {
                isParsed = true;
                options.Port.Should().Be(8080);
                options.IsInbound.Should().BeTrue();
                options.IsOutbound.Should().BeTrue();
            });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Repeated_Hierarchy_Parsing()
        {
            // arrange
            var builder = CreateUtilBuilder();

            // act
            // assert
            {
                int parseCount = 0;
                for (int i = 0; i < 3; i++)
                {
                    var result = builder.Parse("base", "clip sort -o".Split(' '));
                    result.When<SortOptions>(options =>
                    {
                        parseCount++;
                        options.OverWriteClipboard.Should().BeTrue();
                    });
                }

                parseCount.Should().Be(3);
            }
            {
                int parseCount = 0;
                var result = builder.Parse("base", "convert -f png file0.jpg file1.jpg".Split(' '));
                result.When<ConvertOptions>(options =>
                {
                    parseCount++;
                    options.Format.Should().Be("png");
                    options.FileNames.Should().BeEquivalentTo("file0.jpg file1.jpg".Split(' '));
                });
                parseCount.Should().Be(1);
            }
            {
                int parseCount = 0;
                var result = builder.Parse("base", "clip zip file.zip *.jpg *.png".Split(' '));
                result.When<ZipOptions>(options =>
                {
                    parseCount++;
                    options.ZipFileName.Should().Be("file.zip");
                    options.FilterGlobs.Should().BeEquivalentTo("*.jpg *.png".Split(' '));
                });
                parseCount.Should().Be(1);
            }
        }

        private static GitBuilder CreateUtilBuilder()
        {
            var builder = new GitBuilder();
            builder.AddParser<UtilityOptions>("base")
                .WithBooleanSwitch('h', "help", options => options.IsHelpRequested = true)
                .Build()
                .AddParser<ClipboardOptions>("clip")
                .WithBooleanSwitch('o', "overwrite", options => options.OverWriteClipboard = true)
                .Build()
                .AddParser<SortOptions>("sort")
                .WithBooleanSwitch('r', "reverse", options => options.IsReversed = true)
                .WithFactoryFunctions(() => new SortOptions())
                .Build()
                .AddParser<ZipOptions>("zip")
                .WithPositional((o, s) => o.ZipFileName = s)
                .WithPositionals((o, strings) => o.FilterGlobs = strings)
                .WithFactoryFunctions(() => new ZipOptions())
                .Build()
                .AddParser<ConvertOptions>("convert")
                .WithSingleValueSwitch('f', "format", (options, s) => options.Format = s)
                .WithPositionals((o, strings) => o.FileNames = strings)
                .WithFactoryFunctions(() => new ConvertOptions())
                .Build()
                .AddParser<FireWallOptions>("firewall")
                .WithSingleValueSwitch('p', "port", (o, s) => o.Port = Convert.ToInt32(s))
                .WithSingleValueSwitch('m', "mode", (o, s) =>
                {
                    if (s.Contains("i"))
                        o.IsInbound = true;
                    if (s.Contains("o"))
                        o.IsOutbound = true;
                })
                .WithPositional((o, s) => o.Program = s)
                .Build()
                .AddParser<BlockProgramOptions>("block")
                .WithFactoryFunctions(() => new BlockProgramOptions())
                .Build()
                .AddParser<UnblockProgramOptions>("unblock")
                .WithFactoryFunctions(() => new UnblockProgramOptions())
                .Build()
                .AddSubCommand("base", "clip")
                .AddSubCommand("base", "convert")
                .AddSubCommand("base", "firewall")
                .AddSubCommand("clip", "sort")
                .AddSubCommand("clip", "zip")
                .AddSubCommand("firewall", "block")
                .AddSubCommand("firewall", "unblock");
            return builder;
        }
    }
}