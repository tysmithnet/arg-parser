﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using ArgParser.Styles.Default;
using ArgParser.Testing.Common;
using Newtonsoft.Json;

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
                    .AddParser<UtilOptions>("base")
                    .WithBooleanSwitch('h', "help", o => o.IsHelpRequested = true)
                    .WithBooleanSwitch(null, "version", o => o.IsVersionRequested = true)
                    .Finish
                    .AddParser<ClipboardOptions>("clip")
                    .WithBooleanSwitch('o', "overwrite", o => o.IsOverwriteClipboard = true)
                    .Finish
                    .AddParser<SortOptions>("sort")
                    .WithFactoryFunction(() => new SortOptions())
                    .WithBooleanSwitch('r', "reverse", o => o.IsReversed = true)
                    .Finish
                    .AddParser<ZipOptions>("zip")
                    .WithFactoryFunction(() => new ZipOptions())
                    .WithPositional((o, s) => o.ZipFile = s)
                    .WithPositionals((o, s) => o.Globs = s)
                    .Finish
                    .AddParser<FireWallOptions>("firewall")
                    .WithSingleValueSwitch('p', "port", (o, s) => o.Port = Convert.ToInt32(s))
                    .WithSingleValueSwitch('m', "mode", (o, s) =>
                    {
                        o.IsInbound = s.Contains("i");
                        o.IsOutbound = s.Contains("o");
                    })
                    .WithPositional((o, s) => o.Program = s)
                    .Finish
                    .AddParser<BlockProgramOptions>("block")
                    .WithFactoryFunction(() => new BlockProgramOptions())
                    .Finish
                    .AddParser<UnblockProgramOptions>("unblock")
                    .WithFactoryFunction(() => new UnblockProgramOptions())
                    .Finish
                    .AddParser<ConvertOptions>("convert")
                    .WithFactoryFunction(() => new ConvertOptions())
                    .WithSingleValueSwitch('f', "format", (o, s) => o.Format = s)
                    .WithPositionals((o, s) => o.InputFiles = s)
                    .Finish
                    .CreateParentChildRelationship("base", "clip")
                    .CreateParentChildRelationship("base", "firewall")
                    .CreateParentChildRelationship("base", "convert")
                    .CreateParentChildRelationship("clip", "sort")
                    .CreateParentChildRelationship("clip", "zip")
                    .CreateParentChildRelationship("firewall", "block")
                    .CreateParentChildRelationship("firewall", "unblock");

                var result = builder.Parse("base", args);
                result.When<UtilOptions>(options =>
                {
                    Console.WriteLine(options.GetType().FullName);
                    Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));
                });

                result.WhenError(exceptions =>
                {
                    Console.Error.WriteLine("Error");
                    Console.WriteLine(JsonConvert.SerializeObject(exceptions, Formatting.Indented));
                });
            }
        }
    }
}