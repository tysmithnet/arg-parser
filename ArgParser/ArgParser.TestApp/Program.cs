﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using Alba.CsConsoleFormat;
using ArgParser.Core;
using ArgParser.Styles;
using ArgParser.Styles.Alba;
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

        private static void FindHelp(UtilOptions options, Context context)
        {
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter commands for the fake tool `util` e.g. firewall -h");
            while (true)
            {
                var builder = DefaultBuilder.CreateDefaultBuilder();
                var context = builder.BuildContext();
                var template = new DefaultTemplate(context, new DefaultTemplateVm(context.ParserRepository.Get("firewall"), new AlbaContext(context), Theme.Cool));
                var doc = template.Create();
                ConsoleRenderer.RenderDocument(doc);
                Console.ReadLine();
                continue;
                Console.Write($"$ util ");
                var line = Console.ReadLine();
                if (line.IsNullOrWhiteSpace())
                    continue;
                args = CommandLineToArgs(line).Where(x => !x.IsNullOrWhiteSpace()).ToArray();
                if (args.Length == 0)
                    continue;

                
                
                var result = builder.Parse("util", args);
                result.When<UtilOptions>(options =>
                {
                    Console.WriteLine(options.GetType().FullName);
                    Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));
                    if (!options.IsHelpRequested) return;
                    FindHelp(options, context);
                });

                result.WhenError(exceptions =>
                {
                    exceptions = exceptions.ToList();
                    foreach (var parseException in exceptions)
                        if (parseException is MissingRequiredParameterException mrpe &&
                            mrpe.Instance is UtilOptions options && options.IsHelpRequested)
                        {
                            FindHelp(options, context);
                            return;
                        }

                    Console.Error.WriteLine("Error");
                    foreach (var ex in exceptions)
                    {
                        Console.Error.WriteLineAsync($"Parse Error: {ex.Message}");
                    }
                });
            }
        }
    }

   
}