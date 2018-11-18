namespace ArgParser.Styles.Alba
{
    public interface IThemeRepository
    {
        Theme Get(string parserId);
        Theme Default { get; }
        void SetTheme(string parserId, Theme theme);
    }
}