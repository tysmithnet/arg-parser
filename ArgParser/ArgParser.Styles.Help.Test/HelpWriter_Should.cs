using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
    public class HelpWriter_Should
    {
        [Fact]
        public void Display_A_Heading_On_A_Single_Line()
        {
            // arrange
            var writer = new HelpWriter();
            var root = new RootNode
            {
                Children =
                {
                    new HeadingNode("git"),
                    new TextNode("The dumb content tracker")
                }
            };

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"          _   __                                                                
  ___ _  (_) / /_                                                               
 / _ `/ / / / __/                                                               
 \_, / /_/  \__/                                                                
/___/                                                                           
The dumb content tracker                                                        ");
        }

        [Fact]
        public void Pass_Hello_World_Test()
        {
            // arrange
            var writer = new HelpWriter();
            var root = new RootNode
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

        [Fact]
        public void Render_Grids_As_Tables()
        {
            // arrange
            var writer = new HelpWriter();
            var root = new RootNode
            {
                Children =
                {
                    new GridNode
                    {
                        Columns = 3,
                        Children =
                        {
                            new TextNode("a"),
                            new TextNode("b"),
                            new TextNode("c"),
                            new TextNode("d"),
                            new TextNode("e"),
                            new TextNode("f")
                        }
                    }
                }
            };
            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"╔═╤═╤═╗                                                                         
║a│b│c║                                                                         
╟─┼─┼─╢                                                                         
║d│e│f║                                                                         
╚═╧═╧═╝                                                                         ");
        }
    }
}