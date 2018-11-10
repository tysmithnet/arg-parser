using System;
using ArgParser.Styles.Default;

namespace ArgParser.Testing.Common
{
    public class DefaultBuilder
    {
        public static ContextBuilder Create()
        {
            return new ContextBuilder()
                .AddRoot<UtilOptions>(help =>
                {
                    help
                        .SetName("utility")
                        .SetVersion("1.0.0.0")
                        .SetShortDescription("General utility tool")
                        .SetLongDescription("A collection of small utilities used frequently.");
                })
                .WithFactoryFunction(() => new UtilOptions())
                .WithBooleanSwitch('h', "help", o => o.IsHelpRequested = true, help =>
                {
                    help
                        .SetName("Help")
                        .SetShortDescription("Get help on commands");
                })
                .WithBooleanSwitch(null, "version", o => o.IsVersionRequested = true, help =>
                {
                    help
                        .SetName("Version")
                        .SetShortDescription("Display the current version");
                })
                .Finish
                .AddParser<ClipboardOptions>("clip", help =>
                {
                    help
                        .SetName("Clipboard")
                        .SetShortDescription("Interact with the clipboard");
                })
                .WithFactoryFunction(() => new ClipboardOptions())
                .WithBooleanSwitch('o', "overwrite", o => o.IsOverwriteClipboard = true, help =>
                {
                    help
                        .SetName("Overwrite")
                        .SetShortDescription("Overwrite the contents of the clipboard");
                })
                .Finish
                .AddParser<SortOptions>("sort", help =>
                {
                    help
                        .SetName("Sort")
                        .SetShortDescription("Sort the lines of text on the clipboard");
                })
                .WithFactoryFunction(() => new SortOptions())
                .WithBooleanSwitch('r', "reverse", o => o.IsReversed = true, help =>
                {
                    help
                        .SetName("Reverse")
                        .SetShortDescription("Reverse the lines of sorted text");
                })
                .Finish
                .AddParser<ZipOptions>("zip", help =>
                {
                    help
                        .SetName("Zip")
                        .SetShortDescription("Zip the files currently on the clipboard");
                })
                .WithFactoryFunction(() => new ZipOptions())
                .WithPositional((o, s) => o.ZipFile = s, help =>
                {
                    help
                        .SetName("Output File")
                        .SetShortDescription("The zip file to create");
                })
                .WithPositionals((o, s) => o.Globs = s, helpSetupCallback: help =>
                {
                    help
                        .SetName("Glob Patterns")
                        .SetShortDescription("Optional list of glob patterns to use to zip only some of the files");
                })
                .Finish
                .AddParser<FireWallOptions>("firewall", help =>
                {
                    help
                        .SetName("Firewall")
                        .SetShortDescription("Interact with the the local firewall");
                })
                .WithFactoryFunction(() => new FireWallOptions())
                .WithSingleValueSwitch('p', "port", (o, s) => o.Port = Convert.ToInt32(s), help =>
                {
                    help
                        .SetName("Port")
                        .SetShortDescription("The port on which to act");
                })
                .WithSingleValueSwitch('m', "mode", (o, s) =>
                {
                    o.IsInbound = s.Contains("i");
                    o.IsOutbound = s.Contains("o");
                }, help =>
                {
                    help
                        .SetName("Mode")
                        .SetShortDescription("Set whether inbound or outbound traffic should be blocked");
                })
                .WithPositional((o, s) => o.Program = s, help =>
                {
                    help
                        .SetName("Program")
                        .SetShortDescription("Which program to set the rule on");
                })
                .Finish
                .AddParser<BlockProgramOptions>("block", help =>
                {
                    help
                        .SetName("Block")
                        .SetShortDescription("Block a program in/out on a specified port");
                })
                .WithFactoryFunction(() => new BlockProgramOptions())
                .Finish
                .AddParser<UnblockProgramOptions>("unblock", help =>
                {
                    help
                        .SetName("Unblock")
                        .SetShortDescription("Unblock a program in/out on a specified port");
                })
                .WithFactoryFunction(() => new UnblockProgramOptions())
                .Finish
                .AddParser<ConvertOptions>("convert", help =>
                {
                    help
                        .SetName("Convert")
                        .SetShortDescription("Convert files to another format");
                })
                .WithFactoryFunction(() => new ConvertOptions())
                .WithSingleValueSwitch('f', "format", (o, s) => o.Format = s, help =>
                {
                    help
                        .SetName("Format")
                        .SetShortDescription("What format to conver the files to");
                })
                .WithPositionals((o, s) => o.InputFiles = s, helpSetupCallback: help =>
                {
                    help
                        .SetName("Input Files")
                        .SetShortDescription("Input files to convert");
                })
                .Finish
                .CreateParentChildRelationship("clip")
                .CreateParentChildRelationship("firewall")
                .CreateParentChildRelationship("convert")
                .CreateParentChildRelationship("clip", "sort")
                .CreateParentChildRelationship("clip", "zip")
                .CreateParentChildRelationship("firewall", "block")
                .CreateParentChildRelationship("firewall", "unblock");
        }
    }
}