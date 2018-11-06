using System;
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
                    .AddParser<UtilOptions>("base")
                    .WithBooleanSwitch('h', "help", o => o.IsHelpRequested = true)
                    .WithBooleanSwitch(null, "version", o => o.IsVersionRequested = true)
                    .Finish
                    .AddParser<ClipboardOptions>("clip")
                    .WithBooleanSwitch('o', "overwrite", o => o.IsOverwriteClipboard = true)
                    .Finish
                    .AddParser<SortOptions>("sort")
                    .WithBooleanSwitch('r', "reverse", o => o.IsReversed = true)
                    .Finish
                    .AddParser<ZipOptions>("zip");
            }
        }
    }
}
