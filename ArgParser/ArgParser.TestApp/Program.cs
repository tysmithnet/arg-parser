using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using ArgParser.Styles.Help;
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
                var builder = DefaultBuilder.CreateDefaultBuilder();
                var context = builder.BuildContext();
                var result = builder.Parse("util", args);
                result.When<UtilOptions>(options =>
                {
                    Console.WriteLine(options.GetType().FullName);
                    Console.WriteLine(JsonConvert.SerializeObject(options, Formatting.Indented));
                    if (!options.IsHelpRequested) return;
                    if (options.GetType() == typeof(UtilOptions))
                        context.RenderHelp("util", Console.WindowWidth);
                    else if (options.GetType() == typeof(ClipboardOptions))
                        context.RenderHelp("clip", Console.WindowWidth);
                    else if (options.GetType() == typeof(SortOptions))
                        context.RenderHelp("sort", Console.WindowWidth);
                    else if (options.GetType() == typeof(ZipOptions))
                        context.RenderHelp("zip", Console.WindowWidth);
                    else if (options.GetType() == typeof(FireWallOptions))
                        context.RenderHelp("firewall");
                    else if (options.GetType() == typeof(BlockProgramOptions))
                        context.RenderHelp("block", Console.WindowWidth);
                    else if (options.GetType() == typeof(UnblockProgramOptions))
                        context.RenderHelp("unblock", Console.WindowWidth);
                    else if (options.GetType() == typeof(ConvertOptions))
                        context.RenderHelp("convert", Console.WindowWidth);
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