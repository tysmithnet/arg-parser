using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using ArgParser.Flavors.Git;
using Newtonsoft.Json;

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
        public string[] Globs { get; set; }
        public string ZipFile { get; set; }
    }

    public class ConvertOptions : UtilOptions
    {
        public string Format { get; set; }
        public string[] InputFiles { get; set; }
    }

    public abstract class FireWallOptions : UtilOptions
    {
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public int Port { get; set; }
        public string Program { get; set; }
    }

    public class BlockProgramOptions : FireWallOptions
    {
    }

    public class UnblockProgramOptions : FireWallOptions
    {
    }

    internal class Program
    {
        public static string[] CommandLineToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Write($"Enter command line: ");
                var line = Console.ReadLine();
                args = CommandLineToArgs(line);
                var builder = new GitBuilder()
                    .AddParser<UtilOptions>("util")
                    .WithBooleanSwitch('h', "help", options => options.IsHelpRequested = true)
                    .WithBooleanSwitch(null, "version", options => options.IsVersionRequested = true)
                    .Build()
                    .AddParser<ClipboardOptions>("clip")
                    .WithBooleanSwitch('o', "overwrite", options => options.IsOverwriteClipboard = true)
                    .Build()
                    .AddParser<SortOptions>("sort")
                    .WithBooleanSwitch('r', "reverse", options => options.IsReversed = true)
                    .WithFactoryFunctions(() => new SortOptions())
                    .Build()
                    .AddParser<ZipOptions>("zip")
                    .WithPositional((o, s) => o.ZipFile = s)
                    .WithPositionals((options, strings) => options.Globs = strings)
                    .WithFactoryFunctions(() => new ZipOptions())
                    .Build()
                    .AddParser<ConvertOptions>("convert")
                    .WithValuesSwitch('i', "input", (options, strings) => options.InputFiles = strings)
                    .WithSingleValueSwitch('f', "format", (options, s) => options.Format = s)
                    .WithFactoryFunctions(() => new ConvertOptions())
                    .Build()
                    .AddParser<FireWallOptions>("firewall")
                    .WithSingleValueSwitch('p', "port", (options, s) => options.Port = Convert.ToInt32(s))
                    .WithSingleValueSwitch('m', "mode", (options, s) =>
                    {
                        options.IsInbound = s.Contains("i");
                        options.IsOutbound = s.Contains("o");
                    })
                    .WithPositional((o, s) => o.Program = s)
                    .Build()
                    .AddParser<BlockProgramOptions>("block")
                    .WithFactoryFunctions(() => new BlockProgramOptions())
                    .Build()
                    .AddParser<UnblockProgramOptions>("unblock")
                    .WithFactoryFunctions(() => new UnblockProgramOptions())
                    .Build()
                    .AddSubCommand("util", "clip")
                    .AddSubCommand("util", "convert")
                    .AddSubCommand("util", "firewall")
                    .AddSubCommand("clip", "sort")
                    .AddSubCommand("clip", "zip")
                    .AddSubCommand("firewall", "block")
                    .AddSubCommand("firewall", "unblock");

                builder.Parse("util", args)
                    .When<UtilOptions>(options =>
                    {
                        Console.WriteLine($"Parsed: {options.GetType().FullName}");
                        var json = JsonConvert.SerializeObject(options, Formatting.Indented);
                        Console.WriteLine(json);
                    });
            }
        }
    }
}