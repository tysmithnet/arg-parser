using System;

namespace ArgParser.Styles.Alba
{
    public abstract class Theme
    {
        public static readonly Theme Basic = new BasicTheme();
        public static readonly Theme Cool = new CoolTheme();
        public static readonly Theme Default = new DefaultTheme();
        public static readonly Theme Warm = new WarmTheme();

        public static Theme Create(ConsoleColor defaultText = ConsoleColor.White,
            ConsoleColor secondaryText = ConsoleColor.White, ConsoleColor codeText = ConsoleColor.White,
            ConsoleColor requiredColor = ConsoleColor.White, ConsoleColor borderColor = ConsoleColor.White) =>
            new DefaultTheme
            {
                DefaultTextColor = defaultText,
                SecondaryTextColor = secondaryText,
                CodeColor = codeText,
                RequiredColor = requiredColor,
                BorderColor = borderColor
            };

        public virtual ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.White;
        public virtual ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.White;

        private class BasicTheme : Theme
        {
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.Magenta;
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.Yellow;
            public override ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.Green;
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Red;
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.Blue;
        }

        private class CoolTheme : Theme
        {
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.DarkCyan;
            public override ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.DarkBlue;
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Cyan;
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.Cyan;
        }

        private class DefaultTheme : Theme
        {
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.Yellow;
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.Green;
            public override ConsoleColor RequiredColor { get; protected internal set; } = ConsoleColor.Red;
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.Yellow;
        }

        private class WarmTheme : Theme
        {
            public override ConsoleColor BorderColor { get; protected internal set; } = ConsoleColor.DarkYellow;
            public override ConsoleColor CodeColor { get; protected internal set; } = ConsoleColor.Red;
            public override ConsoleColor DefaultTextColor { get; protected internal set; } = ConsoleColor.Yellow;
            public override ConsoleColor SecondaryTextColor { get; protected internal set; } = ConsoleColor.DarkRed;
        }
    }
}