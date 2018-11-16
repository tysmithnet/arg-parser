using System;
using System.Collections.Generic;
using System.Text;

namespace ArgParser.Styles
{
    public abstract class Theme
    {
        public static readonly Theme Default = new DefaultTheme();
        public static readonly Theme Cool = new CoolTheme();
        public static readonly Theme Warm = new WarmTheme();
        public virtual ConsoleColor DefaultText { get; } = ConsoleColor.White;
        public virtual ConsoleColor SecondaryText { get; } = ConsoleColor.White;
        public virtual ConsoleColor CodeText { get; } = ConsoleColor.White;
        public virtual ConsoleColor RequiredColor { get; } = ConsoleColor.White;
        public virtual ConsoleColor BorderColor { get; } = ConsoleColor.White;

        private class DefaultTheme : Theme
        {
            public override ConsoleColor SecondaryText { get; } = ConsoleColor.Yellow;
            public override ConsoleColor CodeText { get; } = ConsoleColor.Green;
            public override ConsoleColor RequiredColor { get; } = ConsoleColor.Red;
            public override ConsoleColor BorderColor { get; } = ConsoleColor.Yellow;
        }

        private class CoolTheme : Theme
        {
            public override ConsoleColor DefaultText { get; } = ConsoleColor.DarkBlue;
            public override ConsoleColor SecondaryText { get; } = ConsoleColor.Cyan;
            public override ConsoleColor CodeText { get; } = ConsoleColor.DarkCyan;
            public override ConsoleColor RequiredColor { get; } = ConsoleColor.Cyan;
        }

        private class WarmTheme : Theme
        {
            public override ConsoleColor DefaultText { get; } = ConsoleColor.Yellow;
            public override ConsoleColor SecondaryText { get; } = ConsoleColor.DarkRed;
            public override ConsoleColor CodeText { get; } = ConsoleColor.Red;
            public override ConsoleColor BorderColor { get; } = ConsoleColor.DarkYellow;
        }
    }
}
