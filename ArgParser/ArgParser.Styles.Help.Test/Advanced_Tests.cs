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
A collection of small utilities used frequently.                                
------------------------------                                                  
╔════════╤═════════════════════════════════════════════════════════════════════╗
║clip    │Interact with the clipboard                                          ║
╟────────┼─────────────────────────────────────────────────────────────────────╢
║firewall│Interact with the the local firewall                                 ║
╟────────┼─────────────────────────────────────────────────────────────────────╢
║convert │Convert files to another format                                      ║
╚════════╧═════════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔══════════╤═══════════════════════════════════════════════════════════════════╗
║-h, --help│Get help on commands                                               ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║--version │Display the current version                                        ║
╚══════════╧═══════════════════════════════════════════════════════════════════╝");
        }

        [Fact]
        public void Use_Case_1()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("clip", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"        __   _                                                                  
 ____  / /  (_)   ___                                                           
/ __/ / /  / /   / _ \                                                          
\__/ /_/  /_/   / .__/                                                          
               /_/                                                              
------------------------------                                                  
Usage: clip [sort|zip] [-ho] [--version]                                        
                                                                                
------------------------------                                                  
╔════╤═════════════════════════════════════════════════════════════════════════╗
║sort│Sort the lines of text on the clipboard                                  ║
╟────┼─────────────────────────────────────────────────────────────────────────╢
║zip │Zip the files currently on the clipboard                                 ║
╚════╧═════════════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔═══════════════╤══════════════════════════════════════════════════════════════╗
║-o, --overwrite│Overwrite the contents of the clipboard                       ║
╚═══════════════╧══════════════════════════════════════════════════════════════╝");
        }

        [Fact]
        public void Use_Case_2()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("firewall", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"   ___   _                             __   __                                  
  / _/  (_)  ____ ___  _    __ ___ _  / /  / /                                  
 / _/  / /  / __// -_)| |/|/ // _ `/ / /  / /                                   
/_/   /_/  /_/   \__/ |__,__/ \_,_/ /_/  /_/                                    
                                                                                
------------------------------                                                  
Usage: firewall [block|unblock] [-h] [--version] [-mp v1] [p1]                  
                                                                                
------------------------------                                                  
╔═══════╤══════════════════════════════════════════════════════════════════════╗
║block  │Block a program in/out on a specified port                            ║
╟───────┼──────────────────────────────────────────────────────────────────────╢
║unblock│Unblock a program in/out on a specified port                          ║
╚═══════╧══════════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔══════════╤═══════════════════════════════════════════════════════════════════╗
║-p, --port│The port on which to act                                           ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║-m, --mode│Set whether inbound or outbound traffic should be blocked          ║
╚══════════╧═══════════════════════════════════════════════════════════════════╝");
        }
    }
}