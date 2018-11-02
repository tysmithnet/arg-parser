using ArgParser.Flavors.Git;
using ArgParser.IntegrationTests.Options;
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
    }
}