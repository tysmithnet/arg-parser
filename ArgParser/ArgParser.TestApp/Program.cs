﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Styles.Default;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace ArgParser.TestApp
{
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
                var builder = new ContextBuilder()
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
                    .WithPositional((o, s) => o.ZipFile = s, isRequired: true)
                    .WithPositionals((options, strings) => options.Globs = strings)
                    .WithFactoryFunctions(() => new ZipOptions())
                    .Build()
                    .AddParser<ConvertOptions>("convert")
                    .WithValuesSwitch('i', "input", (options, strings) => options.InputFiles = strings, isRequired: true)
                    .WithSingleValueSwitch('f', "format", (options, s) => options.Format = s, isRequired: true)
                    .WithFactoryFunctions(() => new ConvertOptions())
                    .Build()
                    .AddParser<FireWallOptions>("firewall")
                    .WithSingleValueSwitch('p', "port", (options, s) => options.Port = Convert.ToInt32(s), isRequired: true)
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
                    })
                    .OnError(errors =>
                    {
                        foreach (var parseError in errors)
                        {
                            Console.Error.WriteLine(parseError.Message);
                        }
                    });
            }
        }
    }
}
