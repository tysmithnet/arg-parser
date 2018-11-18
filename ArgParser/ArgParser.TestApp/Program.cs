using System;
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

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter commands for the fake tool `util` e.g. firewall -h");
            while (true)
            {
                var builder = DefaultBuilder.CreateDefaultBuilder()
                    .AddAutoHelp((parseResults, exceptions) =>
                    {
                        foreach (var kvp in parseResults)
                        {
                            if (kvp.Key is UtilOptions casted && casted.IsHelpRequested)
                                return kvp.Value.Id;
                        }

                        // todo: exception?

                        return null;
                    });
                var context = builder.Context;
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
                    foreach (var parseException in exceptions)
                        if (parseException is MissingRequiredParameterException mrpe &&
                            mrpe.Instance is UtilOptions options && options.IsHelpRequested)
                        {
                            var helpTemplate = new ParserHelpTemplate(context, mrpe.RequiredParameter.Parser.Id);
                            var doc = helpTemplate.Create();
                            ConsoleRenderer.RenderDocument(doc);
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