using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .WithFactoryFunctions(() => new TestOptions())
                .Build()
                .AddSubCommand("base", "test");

            // assert
            var result = builder.Parse("base", "test t0 t1 t2 -c betterconfig".Split(' '));
            int parseCount = 0;
            result.When<TestOptions>(options =>
            {
                parseCount++;
                options.ConfigFile.Should().Be("betterconfig");
                options.TestConfigurations.Should().BeEquivalentTo("t0 t1 t2".Split(' '));
            });
            parseCount.Should().Be(1);
        }
    }
}
