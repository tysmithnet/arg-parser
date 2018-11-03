using System;

namespace ArgParser.ColoredConsole
{
    public class Theme
    {
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public ConsoleColor CodeColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor HeadingColor { get; set; } = ConsoleColor.Cyan;
    }
}
