using Alba.CsConsoleFormat;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ParserHelp_Should
    {
        [Fact]
        public void Render_Help()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var sut = new ParserHelpTemplate(builder.Context, "util");

            // act
            var doc = sut.Create();
            var text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget(),
                new Rect(0, 0, 80, Size.Infinity));

            // assert
            text.Should().Be(@"                                                                                
@@@  @@@  @@@@@@@  @@@  @@@                                                     
@@@  @@@  @@@@@@@  @@@  @@@                                                     
@@!  @@@    @@!    @@!  @@!                                                     
!@!  @!@    !@!    !@!  !@!                                                     
@!@  !@!    @!!    !!@  @!!                                                     
!@!  !!!    !!!    !!!  !!!                                                     
!!:  !!!    !!:    !!:  !!:                                                     
:!:  !:!    :!:    :!:   :!:                                                    
::::: ::     ::     ::   :: ::::                                                
 : :  :      :     :    : :: : :                                                
                                                                                
                                                                                
util - 1.0.0.0 - General utility tool                                           
────────────────────────────────────────────────────────────────────────────────
util [-h, --help][--version]                                                    
┌───────────┬──────────────────────────────────────────────────────────────────┐
│Sub Command│Description                                                       │
├───────────┼──────────────────────────────────────────────────────────────────┤
│clip       │Interact with the clipboard                                       │
├───────────┼──────────────────────────────────────────────────────────────────┤
│firewall   │Interact with the the local firewall                              │
├───────────┼──────────────────────────────────────────────────────────────────┤
│convert    │Convert files to another format                                   │
└───────────┴──────────────────────────────────────────────────────────────────┘
┌────────────┬───┬───────┬─────────────────────────────────────────────────────┐
│Parameter   │Req│Default│Description                                          │
├────────────┼───┼───────┼─────────────────────────────────────────────────────┤
│[-h, --help]│   │false  │Get help on commands                                 │
├────────────┼───┼───────┼─────────────────────────────────────────────────────┤
│[--version] │   │false  │Display the current version                          │
└────────────┴───┴───────┴─────────────────────────────────────────────────────┘
Examples:                                                                       
");
        }
    }
}