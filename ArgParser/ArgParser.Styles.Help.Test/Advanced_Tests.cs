using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
    public class Advanced_Tests
    {
        [Fact]
        public void Use_Case_0()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("util", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"        __    _    __                                                           
 __ __ / /_  (_)  / /                                                           
/ // // __/ / /  / /                                                            
\_,_/ \__/ /_/  /_/                                                             
                                                                                
------------------------------                                                  
Usage: util [clip|firewall|convert] [-h] [--version]                            
                                                                                
------------------------------                                                  
╔════════╤════════════════════════════════════╗                                 
║clip    │Interact with the clipboard         ║                                 
╟────────┼────────────────────────────────────╢                                 
║firewall│Interact with the the local firewall║                                 
╟────────┼────────────────────────────────────╢                                 
║convert │Convert files to another format     ║                                 
╚════════╧════════════════════════════════════╝                                 
------------------------------                                                  
╔══════════╤═══════════════════════════╗                                        
║-h, --help│Get help on commands       ║                                        
╟──────────┼───────────────────────────╢                                        
║--version │Display the current version║                                        
╚══════════╧═══════════════════════════╝                                        ");
        }
    }
}