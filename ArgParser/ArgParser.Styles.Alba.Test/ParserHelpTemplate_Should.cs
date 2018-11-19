using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ParserHelpTemplate_Should
    {
        [Fact]
        public void Create_A_Document()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder()
                .RegisterAlba();
            var parser = builder.Context.ParserRepository.Get("util");
            var parserVm = new ParserViewModel(parser, Theme.Default);
            var template = new ParserHelpTemplate(builder.Context, "util")
            {
                ViewModel = new ParserHelpTemplateViewModel(parserVm.ToEnumerableOfOne().ToList(), new List<ParameterViewModel>(), new List<ParserViewModel>())
            };

            // act
            var doc = template.Create();

            // assert
            doc.Should().NotBeNull();
        }
    }
}