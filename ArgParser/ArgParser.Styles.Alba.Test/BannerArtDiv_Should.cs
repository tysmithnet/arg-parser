using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;
using Figgle;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class BannerArtDiv_Should
    {
        [Fact]
        public void Render_Figlet_Font()
        {
            // arrange
            var div = new BannerArtDiv()
            {
                Font = FiggleFonts.Poison,
                Text = "hi"
            };

            // act
            var elements = div.GenerateVisualElements();

            // assert
            elements.Cast<Span>().Single().Text.Should().Be(@"               
@@@  @@@  @@@  
@@@  @@@  @@@  
@@!  @@@  @@!  
!@!  @!@  !@!  
@!@!@!@!  !!@  
!!!@!!!!  !!!  
!!:  !!!  !!:  
:!:  !:!  :!:  
::   :::   ::  
 :   : :  :    
               
");
        }
    }
}
