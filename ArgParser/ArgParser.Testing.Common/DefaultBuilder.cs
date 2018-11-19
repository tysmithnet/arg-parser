using System;
using ArgParser.Styles;

namespace ArgParser.Testing.Common
{
    public class DefaultBuilder
    {
        public static ContextBuilder CreateDefaultBuilder()
        {
            return new ContextBuilder("util")
                .AddParser<UtilOptions>("util", help =>
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
                        .SetDefaultValue("false")
                        .SetShortDescription("Get help on commands");
                })
                .WithBooleanSwitch(null, "version", o => o.IsVersionRequested = true, help =>
                {
                    help
                        .SetName("Version")
                        .SetDefaultValue("false")
                        .SetShortDescription("Display the current version");
                })
                .Finish
                .AddParser<ClipboardOptions>("clipboard", help =>
                {
                    help
                        .SetName("Clipboard")
                        .SetShortDescription("Interact with the clipboard");
                })
                .WithAlias("clip")
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
                        .SetShortDescription("The zip file to create")
                        .SetValueAlias("outfile");
                }, required: true)
                .WithPositionals((o, s) => o.Globs = s, helpSetupCallback: help =>
                {
                    help
                        .SetName("Glob Patterns")
                        .SetShortDescription("Optional list of glob patterns to use to zip only some of the files")
                        .SetValueAlias("glob");
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
                        .SetShortDescription("The port on which to act")
                        .SetValueAlias("8080");
                }, required: true)
                .WithSingleValueSwitch('m', "mode", (o, s) =>
                {
                    o.IsInbound = s.Contains("i");
                    o.IsOutbound = s.Contains("o");
                }, help =>
                {
                    help
                        .SetName("Mode")
                        .SetShortDescription("Set whether inbound or outbound traffic should be blocked")
                        .SetValueAlias("io");
                })
                .WithPositional((o, s) => o.Program = s, help =>
                {
                    help
                        .SetName("Program")
                        .SetValueAlias("firefox.exe")
                        .SetShortDescription("Which program to set the rule on");
                }, required: true)
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
                        .SetShortDescription("Convert files to another format")
                        .AddExample("Image files", "Convert some images to png", "util convert -f png file0.jpg file1.gif", "Converted (2) files to .png");
                })
                .WithFactoryFunction(() => new ConvertOptions())
                .WithSingleValueSwitch('f', "format", (o, s) => o.Format = s, help =>
                {
                    help
                        .SetName("Format")
                        .SetValueAlias("png")
                        .SetShortDescription("What format to conver the files to");
                }, required: true)
                .WithPositionals((o, s) => o.InputFiles = s, helpSetupCallback: help =>
                {
                    help
                        .SetName("Input Files")
                        .SetValueAlias("file")
                        .SetShortDescription("Input files to convert");
                }, required: true)
                .Finish
                .CreateParentChildRelationship("util", "clipboard")
                .CreateParentChildRelationship("util", "firewall")
                .CreateParentChildRelationship("util", "convert")
                .CreateParentChildRelationship("clipboard", "sort")
                .CreateParentChildRelationship("clipboard", "zip")
                .CreateParentChildRelationship("firewall", "block")
                .CreateParentChildRelationship("firewall", "unblock");
        }
    }
}