using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core.Help;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultHelpBuilder_Should
    {
        [Fact]
        public void Return_The_Correct_Value_For_Null_Object()
        {
            // arrange
            var builder = new DefaultHelpBuilder();

            // act
            // assert
            if (builder.Build() is TextNode textNode)
            {
                textNode.Text.Should().Be(@"");
            }
            else
            {
                true.Should().BeFalse($"Expected text node");
            }
        }

        [Fact]
        public void Return_The_Correct_Value_For_Just_Generic_Help()
        {
            // arrange
            var builder = new DefaultHelpBuilder()
                .AddGenericHelp(new GenericHelp()
                {
                    Author = "@duke",
                    Description = "Duke is a pretty cool corgi. He's a heckin' good boy",
                    ShortDescription = "Duke's program",
                    Name = "duke.exe",
                    Version = "1.2.3.4"
                });

            // act
            // assert
            if (builder.Build() is TextNode textNode)
            {
                textNode.Text.Trim().Should().Be(@"duke.exe - 1.2.3.4 - Duke's program
Duke is a pretty cool corgi. He's a heckin' good boy

Author: @duke");
            }
            else
            {
                true.Should().BeFalse($"Expected text node");
            }
        }

        [Fact]
        public void Return_The_Correct_Value_For_Just_Parameters()
        {
            // arrange
            var builder = new DefaultHelpBuilder()
                .AddParameter("status", new[] {"status pid", "status --all"}, "Get the status of something")
                .AddParameter("log", new[] {"log logfile.txt"}, "Log the output to a file");

            // act
            // assert
            if (builder.Build() is TextNode textNode)
            {
                textNode.Text.Trim().Should().Be(@"status - Get the status of something
    status pid
    status --all

log - Log the output to a file
    log logfile.txt");
            }
            else
            {
                true.Should().BeFalse($"Expected text node");
            }
        }
    }
}
