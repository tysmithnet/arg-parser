using System.Collections.Generic;

namespace ArgParser.Styles.Alba
{
    public class ThemeRepository : IThemeRepository
    {
        public Theme Get(string parserId) => !Themes.ContainsKey(parserId) ? Default : Themes[parserId];

        public void SetTheme(string parserId, Theme theme)
        {
            if (!Themes.ContainsKey(parserId))
                Themes.Add(parserId, theme);
            Themes[parserId] = theme;
        }

        public Theme Default { get; } = Theme.Default;
        protected internal Dictionary<string, Theme> Themes { get; set; } = new Dictionary<string, Theme>();
    }
}