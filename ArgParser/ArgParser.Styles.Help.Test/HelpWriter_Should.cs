using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core.Help;
using ArgParser.HelpWriter;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class HelpWriter_Should
    {
        [Fact]
        public void Return_An_Empty_String_For_An_Empty_Dom()
        {
            // arrange
            var sut = new HelpWriter.HelpWriter();
            var root = new RootNode();

            // act
            var res = sut.CreateHelp(root);

            // assert
            res.Should().Be("");
        }
    }
}
