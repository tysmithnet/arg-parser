using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.ColoredConsole;
using ArgParser.Core.Help.Dom;
using ArgParser.Flavors.Git;

namespace ArgParser.TestConsole
{

    public abstract class UtilOptions
    {
        public bool IsHelpRequested { get; set; }
        public bool IsVersionRequested { get; set; }
    }

    public abstract class ClipboardOptions : UtilOptions
    {
        public bool IsOverwriteClipboard { get; set; }
    }

    public class SortOptions : ClipboardOptions
    {
        public bool IsReversed { get; set; }
    }

    public class ZipOptions : ClipboardOptions
    {
        public string ZipFile { get; set; }
        public string[] Globs { get; set; }
    }

    public class ConvertOptions : UtilOptions
    {
        public string InputFiles { get; set; }
        public string Format { get; set; }
    }

    public abstract class FireWallOptions : UtilOptions
    {
        public int Port { get; set; }
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public string Program { get; set; }
    }

    public class BlockProgram : FireWallOptions
    {

    }

    public class UnblockProgram : FireWallOptions
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new GitBuilder()
                .AddParser<UtilOptions>("util")
                .WithBooleanSwitch('h', "help", options => options.IsHelpRequested = true)
                .WithBooleanSwitch(null, "version", options => options.IsVersionRequested = true)
                .Build();
        }
    }
}
