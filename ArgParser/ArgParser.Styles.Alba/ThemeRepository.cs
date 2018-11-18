using System.Collections.Generic;

namespace ArgParser.Styles.Alba
{
    public class ThemeRepository : IThemeRepository
    {
        protected internal Dictionary<string, Theme> Themes { get; set; } = new Dictionary<string, Theme>();
        public Theme Get(string parserId)
        {
            return Themes[parserId];
        }

        public Theme Default { get; } = Theme.Default;

        public void SetTheme(string parserId, Theme theme)
        {
            if(!Themes.ContainsKey(parserId))
                Themes.Add(parserId, theme);
            Themes[parserId] = theme;
        }
    }
}