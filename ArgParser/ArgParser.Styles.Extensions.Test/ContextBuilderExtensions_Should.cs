using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Testing.Common;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class ContextBuilderExtensions_Should
    {
        [Fact]
        public void Allow_Auto_Help_To_Be_Registered()
        {
            // arrange
            var mockRenderer = new Mock<ITemplateRenderer>();
            mockRenderer.SetupAllProperties();
            var isHelpRequested = false;
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .RegisterExtensions()
                .AddAutoHelp((results, exceptions) =>
                {
                    isHelpRequested = true;
                    var first = results.Keys.OfType<UtilOptions>().FirstOrDefault();
                    if (first != null && first.IsHelpRequested)
                        return results[first].Id;
                    return null;
                }, factory =>
                {
                    factory.TemplateRenderer = mockRenderer.Object;
                    return factory;
                });

            // act
            builder.Parse("-h".Split(' '));

            // assert
            isHelpRequested.Should().BeTrue();
        }

        [Fact]
        public void Allow_Context_Builders_To_Register()
        {
            // arrange
            var contextBuilder = new ContextBuilder()
                .RegisterExtensions();

            // act
            // assert
            ContextBuilderExtensions.ExtensionContexts[contextBuilder.Context].Should().BeOfType<ExtensionContext>();
        }

        [Fact]
        public void Allow_Themes_To_Be_Added_When_Creating_Generic_Parsers()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser<object>("root")
                .WithTheme(Theme.Warm);

            // act
            // assert
            ContextBuilderExtensions.ParserThemes.Single().Value.Should().Be(Theme.Warm);
        }

        [Fact]
        public void Allow_Themes_To_Be_Added_When_Creating_Parsers()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("root")
                .WithTheme(Theme.Warm);

            // act
            // assert
            ContextBuilderExtensions.ParserThemes[builder.Parser].Should().Be(Theme.Warm);
        }

        [Fact]
        public void Allow_Themes_To_Be_Set_After_They_Have_Been_Created()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .SetTheme("util", Theme.Cool);

            // act
            // assert
            ContextBuilderExtensions.ParserThemes[builder.Context.ParserRepository.Get("util")].Should().Be(Theme.Cool);
        }

        [Fact]
        public void Not_Throw_If_Asking_For_An_Unregistered_Context()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            Action mightThrow0 = () => builder.Context.ToExtensionContext();

            // act
            // assert
            mightThrow0.Should().NotThrow();
        }

        [Fact]
        public void Return_The_Same_Extension_Context_When_Asked_Multiple_Times()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .RegisterExtensions();

            // act
            // assert
            var set = new HashSet<ExtensionContext>();
            for (var i = 0; i < 10; i++) set.Add(builder.Context.ToExtensionContext());

            set.Should().HaveCount(1);
        }
    }
}