﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using ArgParser.Core;
using ArgParser.Styles.Extensions;
using ArgParser.Testing.Common;
using Figgle;
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
            Console.WriteLine("Enter commands for the fake tool `util` e.g. firewall -h");
            while (true)
            {
                var builder = DefaultBuilder.CreateDefaultBuilder()
                    .RegisterExtensions()
                    .AddAutoHelp((parseResults, exceptions) =>
                    {
                        foreach (var kvp in parseResults)
                            if (kvp.Key is UtilOptions casted && casted.IsHelpRequested)
                                return kvp.Value.Id;

                        var missingValues = exceptions.OfType<MissingValueException>();
                        var first = missingValues.FirstOrDefault();
                        return first?.Parser.Id;
                    })
                    .SetTheme("util",
                        Theme.Create(FiggleFonts.Doom, ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.Yellow, ConsoleColor.Red,
                            ConsoleColor.Yellow))
                    .SetTheme("clipboard",
                        Theme.Create(FiggleFonts.Alpha, ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.Red,
                            ConsoleColor.Gray))
                    .SetTheme("sort",
                        Theme.Create(FiggleFonts.Banner3D, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Green, ConsoleColor.Red,
                            ConsoleColor.Green))
                    .SetTheme("zip",
                        Theme.Create(FiggleFonts.Banner3D, ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.Cyan, ConsoleColor.Red,
                            ConsoleColor.Cyan))
                    .SetTheme("convert",
                        Theme.Create(FiggleFonts.Banner3D, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.Magenta, ConsoleColor.Red,
                            ConsoleColor.Magenta))
                    .SetTheme("firewall",
                        Theme.Create(FiggleFonts.FireFontK, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.DarkRed,
                            ConsoleColor.DarkYellow))
                    .SetTheme("block",
                        Theme.Create(FiggleFonts.Banner3D, ConsoleColor.Green, ConsoleColor.DarkBlue, ConsoleColor.Yellow, ConsoleColor.Red,
                            ConsoleColor.Yellow))
                    .SetTheme("unblock",
                        Theme.Create(FiggleFonts.Ghost, ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.DarkCyan, ConsoleColor.Red,
                            ConsoleColor.DarkCyan));

                Console.Write($"$ util ");
                var line = Console.ReadLine();
                if (line.IsNullOrWhiteSpace())
                    continue;
                args = CommandLineToArgs(line).Where(x => !x.IsNullOrWhiteSpace()).ToArray();
                if (args.Length == 0)
                    continue;

                var result = builder.Parse(args);
                result.When<UtilOptions>((options, parser) =>
                {
                    Console.WriteLine(options.GetType().FullName);
                    Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));
                });

                result.WhenError(exceptions =>
                {
                    exceptions = exceptions.ToList();
                    Console.Error.WriteLine("Error");
                    foreach (var ex in exceptions) Console.Error.WriteLineAsync($"Parse Error: {ex.Message}");
                });
            }
        }
    }
}