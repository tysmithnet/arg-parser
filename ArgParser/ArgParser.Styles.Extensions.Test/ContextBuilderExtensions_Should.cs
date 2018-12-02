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
        public void Create_A_Theme_Repository_With_All_Added_Themes()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            builder.SetTheme("util", Theme.Cool);

            // act
            var res = builder.Context.ToExtensionContext();

            // assert
            res.ThemeRepository.Get("util").Should().BeSameAs(Theme.Cool);
        }

        [Fact]
        public void Not_Do_Anything_For_Positionals()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            builder.SetSwitchTokens("#", "##");

            // act
            builder.AddParser("whatever").WithPositional((o, s) => { });

            // assert
            builder.Context.ParserRepository.Get("whatever").Should().NotBeNull();
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
                .WithTheme(Theme.Cool)
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
                .SetTheme("util", Theme.Cool)
                .SetTheme("util", Theme.Warm);

            // act
            // assert
            ContextBuilderExtensions.ParserThemes[builder.Context.ParserRepository.Get("util")].Should().Be(Theme.Warm);
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

        [Fact]
        public void Allow_The_Letter_And_Word_Token_To_Be_Changed()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .SetSwitchTokens("#", "#~#");

            // act
            bool isParsed = false;
            builder
                .Parse("#h #~#version".Split(' '))
                .When<UtilOptions>((options, parser) =>
                {
                    isParsed = true;
                    options.IsHelpRequested.Should().BeTrue();
                    options.IsVersionRequested.Should().BeTrue();
                });

            // assert
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Throw_If_Given_Bad_Values_For_SetSwitchTokens()
        {
            // arrange
            var builder = new ContextBuilder();
            Action mightThrow0 = () => builder.SetSwitchTokens(null, "x");
            Action mightThrow1 = () => builder.SetSwitchTokens("x", null);
            Action mightThrow2 = () => builder.SetSwitchTokens(null, "");
            Action mightThrow3 = () => builder.SetSwitchTokens("", null);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentException>();
            mightThrow1.Should().Throw<ArgumentException>();
            mightThrow2.Should().Throw<ArgumentException>();
            mightThrow3.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Set_Switch_Tokens_For_Newly_Created_Parsers0()
        {
            // arrange
            var builder = new ContextBuilder();
            builder.SetSwitchTokens("#", "##");
            builder
                .AddParser("a")
                .WithBooleanSwitch('h', "help", o => {});
            

            // act
            // assert
            var parser = builder.Context.ParserRepository.Get("a");
            var param = parser.Parameters.OfType<Switch>().Single();
            param.LetterToken.Should().Be("#");
            param.WordToken.Should().Be("##");
        }

        [Fact]
        public void Set_Switch_Tokens_For_Newly_Created_Parsers1()
        {
            // arrange
            var builder = new ContextBuilder();
            builder.SetSwitchTokens("#", "##");
            builder
                .AddParser("a", help => { help.SetName("hi"); })
                .WithBooleanSwitch('h', "help", o => { });


            // act
            // assert
            var parser = builder.Context.ParserRepository.Get("a");
            var param = parser.Parameters.OfType<Switch>().Single();
            param.LetterToken.Should().Be("#");
            param.WordToken.Should().Be("##");
        }
    }
}