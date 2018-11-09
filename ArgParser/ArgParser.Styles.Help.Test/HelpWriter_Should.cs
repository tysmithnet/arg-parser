using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
    public class HelpWriter_Should
    {
        [Fact]
        public void Pass_Hello_World_Test()
        {
            // arrange
            var writer = new HelpWriter();
            var root = new RootNode()
            {
                Children =
                {
                    new TextNode("hello world")
                }
            };

            // act
            var res = writer.CreateHelpText(root);
            
            // assert
            res.Should().Be("hello world                                                                     ");
        }
    }
}
