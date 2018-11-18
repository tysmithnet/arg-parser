using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ContextBuilderExtensions_Should
    {
        [Fact]
        public void Allow_Context_Builders_To_Register()
        {
            // arrange
            var contextBuilder = new ContextBuilder("root")
                .RegisterAlba();

            // act
            // assert
            ContextBuilderExtensions.AlbaContexts[contextBuilder.Context].Should().BeOfType<AlbaContext>().Which.Themes
                .Should().HaveCount(0);
        }

        [Fact]
        public void Allow_Auto_Help_To_Be_Registered()
        {
            // arrange
            bool isHelpRequested = false;
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .RegisterAlba()
                .AddAutoHelp((results, exceptions) =>
                {
                    isHelpRequested = true;
                    var first = results.Keys.OfType<UtilOptions>().FirstOrDefault();
                    if (first != null && first.IsHelpRequested)
                        return results[first].Id;
                    return null;
                });

            // act
            builder.Parse("-h".Split(' '));

            // assert
            isHelpRequested.Should().BeTrue();
        }
    }
}
