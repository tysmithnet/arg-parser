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
        public static readonly Theme Basic = new BasicTheme();
        public virtual ConsoleColor DefaultText { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor SecondaryText { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor CodeText { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.White;

        public static Theme Create(ConsoleColor defaultText = ConsoleColor.White,
            ConsoleColor secondaryText = ConsoleColor.White, ConsoleColor codeText = ConsoleColor.White,
            ConsoleColor requiredColor = ConsoleColor.White, ConsoleColor borderColor = ConsoleColor.White)
        {
            return new DefaultTheme()
            {
                DefaultText = defaultText,
                SecondaryText = secondaryText,
                CodeText = codeText,
                RequiredColor = requiredColor,
                BorderColor = borderColor
            };
        }

        private class DefaultTheme : Theme
        {
            public override ConsoleColor SecondaryText { get; protected internal set; } = ConsoleColor.Yellow;
            public override ConsoleColor CodeText { get; protected internal set; } = ConsoleColor.Green;
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Red;
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.Yellow;
        }

        private class CoolTheme : Theme
        {
            public override ConsoleColor DefaultText { get; protected internal set; } = ConsoleColor.DarkBlue;
            public override ConsoleColor SecondaryText { get; protected internal set; } = ConsoleColor.Cyan;
            public override ConsoleColor CodeText { get; protected internal set; } = ConsoleColor.DarkCyan;
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Cyan;
        }

        private class WarmTheme : Theme
        {
            public override ConsoleColor DefaultText { get; protected internal set; } = ConsoleColor.Yellow;
            public override ConsoleColor SecondaryText { get; protected internal set; } = ConsoleColor.DarkRed;
            public override ConsoleColor CodeText { get; protected internal set; } = ConsoleColor.Red;
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.DarkYellow;
        }

        private class BasicTheme : Theme
        {
            public override ConsoleColor DefaultText { get; protected internal set; } = ConsoleColor.Green;
            public override ConsoleColor SecondaryText { get; protected internal set; } = ConsoleColor.Blue;
            public override ConsoleColor CodeText { get; protected internal set; } = ConsoleColor.Yellow;
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Red;
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.Magenta;
        }
    }
}
