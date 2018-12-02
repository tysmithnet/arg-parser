using Alba.CsConsoleFormat;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class ParserHelp_Should
    {
        [Fact]
        public void Render_Util_Help()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var sut = new ParserHelpTemplate(builder.Context, "util");

            // act
            var doc = sut.Create();
            var text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget(),
                new Rect(0, 0, 80, Size.Infinity));

            // assert
            text.Should().Be(@"       __  _ __                                                                 
 __ __/ /_(_) /                                                                 
/ // / __/ / /                                                                  
\_,_/\__/_/_/                                                                   
                                                                                
                                                                                
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

        [Fact]
        public void Render_Convert_Help()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var sut = new ParserHelpTemplate(builder.Context, "convert");

            // act
            var doc = sut.Create();
            var text = ConsoleRenderer.RenderDocumentToText(doc, new TextRenderTarget(),
                new Rect(0, 0, 80, Size.Infinity));

            // assert
            text.Should().Be(@"                            __                                                  
 _______  ___ _  _____ ____/ /_                                                 
/ __/ _ \/ _ \ |/ / -_) __/ __/                                                 
\__/\___/_//_/___/\__/_/  \__/                                                  
                                                                                
                                                                                
convert - Convert files to another format                                       
────────────────────────────────────────────────────────────────────────────────
util convert [-h, --help][--version][-f, --format:png][file1..fileN]            
┌───────────┬──────────────────────────────────────────────────────────────────┐
│Sub Command│Description                                                       │
└───────────┴──────────────────────────────────────────────────────────────────┘
┌──────────────────┬───┬───────┬───────────────────────────────────────────────┐
│Parameter         │Req│Default│Description                                    │
├──────────────────┼───┼───────┼───────────────────────────────────────────────┤
│[-h, --help]      │   │false  │Get help on commands                           │
├──────────────────┼───┼───────┼───────────────────────────────────────────────┤
│[--version]       │   │false  │Display the current version                    │
├──────────────────┼───┼───────┼───────────────────────────────────────────────┤
│[-f, --format:png]│ ✓ │       │What format to conver the files to             │
├──────────────────┼───┼───────┼───────────────────────────────────────────────┤
│[file1..fileN]    │ ✓ │       │Input files to convert                         │
└──────────────────┴───┴───────┴───────────────────────────────────────────────┘
Examples:                                                                       
Image files - Convert some images to png                                        
util convert -f png file0.jpg file1.gif                                         
Converted (2) files to .png                                                     
");
        }
    }
}