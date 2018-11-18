namespace ArgParser.Styles.Alba
{
    public interface IThemeRepository
    {
        Theme Get(string parserId);
        void SetTheme(string parserId, Theme theme);
        Theme Default { get; }
    }
}