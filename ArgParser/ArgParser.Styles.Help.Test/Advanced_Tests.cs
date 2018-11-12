using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
    public class Advanced_Tests
    {
        [Fact]
        public void Block_Options()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("block", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"   ___    __             __                                                     
  / _ )  / / ___  ____  / /__                                                   
 / _  | / / / _ \/ __/ /  '_/                                                   
/____/ /_/  \___/\__/ /_/\_\                                                    
                                                                                
Block - Block a program in/out on a specified port                              
------------------------------                                                  
Usage: [-p, --port 8080][-m, --mode io][-h, --help][--version][firefox.exe]     
                                                                                
------------------------------                                                  
╔══════════╤═══════════════════════════════════════════════════════════════════╗
║SubCommand│Description                                                        ║
╚══════════╧═══════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔═══════╤═══════════════╤══════════════════════════════════════════════════════╗
║Name   │Usage          │Description                                           ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Port   │-p, --port 8080│*req* The port on which to act                        ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Mode   │-m, --mode io  │Set whether inbound or outbound traffic should be     ║
║       │               │blocked                                               ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Help   │-h, --help     │Get help on commands                                  ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Version│--version      │Display the current version                           ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Program│firefox.exe    │*req* Which program to set the rule on                ║
╚═══════╧═══════════════╧══════════════════════════════════════════════════════╝");
        }

        [Fact]
        public void Clip_Help()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("clip", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"  _____   __   _           __                       __                          
 / ___/  / /  (_)   ___   / /  ___  ___ _  ____ ___/ /                          
/ /__   / /  / /   / _ \ / _ \/ _ \/ _ `/ / __// _  /                           
\___/  /_/  /_/   / .__//_.__/\___/\_,_/ /_/   \_,_/                            
                 /_/                                                            
Clipboard - Interact with the clipboard                                         
------------------------------                                                  
Usage: [-o, --overwrite][-h, --help][--version]                                 
                                                                                
------------------------------                                                  
╔══════════╤═══════════════════════════════════════════════════════════════════╗
║SubCommand│Description                                                        ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║sort      │Sort the lines of text on the clipboard                            ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║zip       │Zip the files currently on the clipboard                           ║
╚══════════╧═══════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔═════════╤═══════════════╤════════════════════════════════════════════════════╗
║Name     │Usage          │Description                                         ║
╟─────────┼───────────────┼────────────────────────────────────────────────────╢
║Overwrite│-o, --overwrite│Overwrite the contents of the clipboard             ║
╟─────────┼───────────────┼────────────────────────────────────────────────────╢
║Help     │-h, --help     │Get help on commands                                ║
╟─────────┼───────────────┼────────────────────────────────────────────────────╢
║Version  │--version      │Display the current version                         ║
╚═════════╧═══════════════╧════════════════════════════════════════════════════╝");
        }

        [Fact]
        public void Firewall_Help()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("firewall", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"   ____   _                             __   __                                 
  / __/  (_)  ____ ___  _    __ ___ _  / /  / /                                 
 / _/   / /  / __// -_)| |/|/ // _ `/ / /  / /                                  
/_/    /_/  /_/   \__/ |__,__/ \_,_/ /_/  /_/                                   
                                                                                
Firewall - Interact with the the local firewall                                 
------------------------------                                                  
Usage: [-p, --port 8080][-m, --mode io][-h, --help][--version][firefox.exe]     
                                                                                
------------------------------                                                  
╔══════════╤═══════════════════════════════════════════════════════════════════╗
║SubCommand│Description                                                        ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║block     │Block a program in/out on a specified port                         ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║unblock   │Unblock a program in/out on a specified port                       ║
╚══════════╧═══════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔═══════╤═══════════════╤══════════════════════════════════════════════════════╗
║Name   │Usage          │Description                                           ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Port   │-p, --port 8080│*req* The port on which to act                        ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Mode   │-m, --mode io  │Set whether inbound or outbound traffic should be     ║
║       │               │blocked                                               ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Help   │-h, --help     │Get help on commands                                  ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Version│--version      │Display the current version                           ║
╟───────┼───────────────┼──────────────────────────────────────────────────────╢
║Program│firefox.exe    │*req* Which program to set the rule on                ║
╚═══════╧═══════════════╧══════════════════════════════════════════════════════╝");
        }

        [Fact]
        public void Util_Help()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new HelpNodeFactory();
            var root = fac.Create("util", builder.BuildContext());
            var writer = new HelpWriter();

            // act
            var res = writer.CreateHelpText(root);

            // assert
            res.Should().Be(@"        __    _    __   _   __                                                  
 __ __ / /_  (_)  / /  (_) / /_  __ __                                          
/ // // __/ / /  / /  / / / __/ / // /                                          
\_,_/ \__/ /_/  /_/  /_/  \__/  \_, /                                           
                               /___/                                            
utility 1.0.0.0 - General utility tool                                          
------------------------------                                                  
Usage: [-h, --help][--version]                                                  
A collection of small utilities used frequently.                                
------------------------------                                                  
╔══════════╤═══════════════════════════════════════════════════════════════════╗
║SubCommand│Description                                                        ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║clip      │Interact with the clipboard                                        ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║firewall  │Interact with the the local firewall                               ║
╟──────────┼───────────────────────────────────────────────────────────────────╢
║convert   │Convert files to another format                                    ║
╚══════════╧═══════════════════════════════════════════════════════════════════╝
------------------------------                                                  
╔═══════╤══════════╤═══════════════════════════════════════════════════════════╗
║Name   │Usage     │Description                                                ║
╟───────┼──────────┼───────────────────────────────────────────────────────────╢
║Help   │-h, --help│Get help on commands                                       ║
╟───────┼──────────┼───────────────────────────────────────────────────────────╢
║Version│--version │Display the current version                                ║
╚═══════╧══════════╧═══════════════════════════════════════════════════════════╝");
        }
    }
}