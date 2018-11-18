using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Testing.Common;
using FluentAssertions;
using Moq;
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
            var mockRenderer = new Mock<ITemplateRenderer>();
            mockRenderer.SetupAllProperties();
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
        public void Throw_If_Asking_For_An_Unregistered_Context()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            Action mightThrow0 = () => builder.Context.ToAlbaContext();

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Return_The_Same_Alba_Context_When_Asked_Multiple_Times()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .RegisterAlba();

            // act
            // assert
            var set = new HashSet<AlbaContext>();
            for (int i = 0; i < 10; i++)
            {
                set.Add(builder.Context.ToAlbaContext());
            }

            set.Should().HaveCount(1);
        }

        [Fact]
        public void Allow_Themes_To_Be_Added_When_Creating_Parsers()
        {
            // arrange
            var builder = new ContextBuilder("root")
                .AddParser("root")
                .WithTheme(Theme.Warm);

            // act
            // assert
            ContextBuilderExtensions.ParserThemes[builder.Parser].Should().Be(Theme.Warm);
        }

        [Fact]
        public void Allow_Themes_To_Be_Added_When_Creating_Generic_Parsers()
        {
            // arrange
            var builder = new ContextBuilder("root")
                .AddParser<object>("root")
                .WithTheme(Theme.Warm);

            // act
            // assert
            ContextBuilderExtensions.ParserThemes.Single().Value.Should().Be(Theme.Warm);
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
    }
}
